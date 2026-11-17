Imports System.Diagnostics
Imports System.Threading

Public Class PerformanceSample
    Public Property SampledAt As DateTime
    Public Property CpuPercent As Double
    Public Property MemoryMB As Double
    Public Property RequestsPerSec As Double
    Public Property AvgResponseMs As Double
End Class

Public Class PerformanceSampler
    Private Shared _cpuCounter As PerformanceCounter
    Private Shared _requestCount As Integer = 0
    Private Shared _responseTotalMs As Long = 0
    Private Shared _responseCount As Integer = 0
    Private Shared _errorTimes As New List(Of DateTime)()
    Private Shared ReadOnly _sync As New Object()
    Private Shared ReadOnly _takeSampleLock As New Object()
    Private Shared _lastTakeSampleTime As DateTime = DateTime.MinValue
    Private Shared _lastReqSampleTime As DateTime = DateTime.MinValue

    Private Shared _lastCpuTime As TimeSpan
    Private Shared _lastFallbackSampleTime As DateTime
    Private Shared _hasBaseline As Boolean = False

    ' Detect spurious 100% readings
    Private Shared _consecutiveHighCpu As Integer = 0

    Public Shared ReadOnly Samples As New List(Of PerformanceSample)()

    Public Shared CurrentCpu As Double = 0
    Public Shared CurrentMemoryMB As Double = 0
    Public Shared CurrentReqPerSec As Double = 0
    Public Shared CurrentAvgRespMs As Double = 0
    Public Shared CurrentErrorsLastHour As Integer = 0
    Public Shared LastError As String = ""
    Public Shared CounterReady As Boolean = False
    Public Shared Version As String = "v3"

    Public Shared Sub Initialize()
        If _cpuCounter IsNot Nothing Then Return
        Try
            _cpuCounter = New PerformanceCounter("Processor", "% Processor Time", "_Total", True)
            _cpuCounter.NextValue()
            CounterReady = True
        Catch ex As Exception
            _cpuCounter = Nothing
            CounterReady = False
            LastError = "PerformanceCounter unavailable: " & ex.Message
        End Try
    End Sub

    Public Shared Sub RecordRequest()
        Interlocked.Increment(_requestCount)
    End Sub

    Public Shared Sub RecordResponse(elapsedMs As Long)
        Interlocked.Add(_responseTotalMs, elapsedMs)
        Interlocked.Increment(_responseCount)
    End Sub

    Public Shared Sub RecordError()
        SyncLock _sync
            _errorTimes.Add(DateTime.Now)
            If _errorTimes.Count > 500 Then _errorTimes.RemoveAt(0)
        End SyncLock
    End Sub

    Public Shared Sub TakeSample()
        Dim now As DateTime = DateTime.Now
        Dim secondsSinceLast As Double = 0

        SyncLock _takeSampleLock
            Dim msSinceLast As Double = (now - _lastTakeSampleTime).TotalMilliseconds
            If msSinceLast < 1500 AndAlso _lastTakeSampleTime <> DateTime.MinValue Then Return
            secondsSinceLast = Math.Max(0.5, msSinceLast / 1000.0)
            If _lastTakeSampleTime = DateTime.MinValue Then secondsSinceLast = 1.0
            _lastTakeSampleTime = now
        End SyncLock

        ' ===== CPU with 100%-bug detection =====
        Dim rawCpu As Double = 0
        If _cpuCounter IsNot Nothing Then
            Try
                rawCpu = _cpuCounter.NextValue()
            Catch
                rawCpu = -1
            End Try
        Else
            rawCpu = -1
        End If

        ' Detect spurious 100%
        If rawCpu >= 99 Then
            _consecutiveHighCpu += 1
        Else
            _consecutiveHighCpu = 0
        End If

        ' If we've seen 100% 3 times in a row, it's the bug — use fallback
        Dim cpu As Double
        If rawCpu < 0 OrElse _consecutiveHighCpu >= 3 Then
            cpu = GetCpuFallback()
        Else
            cpu = rawCpu
        End If

        If cpu < 0 Then cpu = 0
        If cpu > 100 Then cpu = 100
        CurrentCpu = cpu

        ' ===== Memory =====
        Try
            Using proc As Process = Process.GetCurrentProcess()
                CurrentMemoryMB = proc.WorkingSet64 / (1024.0 * 1024.0)
            End Using
        Catch
            CurrentMemoryMB = 0
        End Try

        ' ===== Requests/sec =====
        Dim reqs As Integer = Interlocked.Exchange(_requestCount, 0)
        CurrentReqPerSec = reqs / secondsSinceLast

        ' ===== Response time =====
        Dim totalMs As Long = Interlocked.Exchange(_responseTotalMs, 0)
        Dim cnt As Integer = Interlocked.Exchange(_responseCount, 0)
        If cnt > 0 Then
            CurrentAvgRespMs = totalMs / cnt
        Else
            CurrentAvgRespMs = 0
        End If

        ' ===== Errors =====
        SyncLock _sync
            _errorTimes.RemoveAll(Function(d) (DateTime.Now - d).TotalHours > 1)
            CurrentErrorsLastHour = _errorTimes.Count
        End SyncLock

        ' ===== Store sample =====
        Dim s As New PerformanceSample()
        s.SampledAt = now
        s.CpuPercent = CurrentCpu
        s.MemoryMB = CurrentMemoryMB
        s.RequestsPerSec = CurrentReqPerSec
        s.AvgResponseMs = CurrentAvgRespMs

        SyncLock _sync
            Samples.Add(s)
            Do While Samples.Count > 150
                Samples.RemoveAt(0)
            Loop
        End SyncLock
    End Sub

    ''' <summary>
    ''' Ensures at least `minimumSamples` entries exist so charts can draw immediately.
    ''' Works even when Samples is empty (uses current values as placeholder).
    ''' </summary>
    Public Shared Sub SeedIfNeeded(minimumSamples As Integer)
        SyncLock _sync
            If Samples.Count >= minimumSamples Then Return

            Dim seedCpu As Double = CurrentCpu
            Dim seedMem As Double = CurrentMemoryMB

            If Samples.Count = 0 Then
                ' Nothing to duplicate — seed with current values as flat baseline
                For i As Integer = 1 To minimumSamples
                    Dim filler As New PerformanceSample()
                    filler.SampledAt = DateTime.Now.AddSeconds(-2 * (minimumSamples - i))
                    filler.CpuPercent = seedCpu
                    filler.MemoryMB = seedMem
                    filler.RequestsPerSec = 0
                    filler.AvgResponseMs = 0
                    Samples.Add(filler)
                Next
            Else
                ' Duplicate the last sample backwards
                Dim last As PerformanceSample = Samples(Samples.Count - 1)
                Dim toAdd As Integer = minimumSamples - Samples.Count
                For i As Integer = 1 To toAdd
                    Dim filler As New PerformanceSample()
                    filler.SampledAt = last.SampledAt.AddSeconds(-2 * (toAdd - i + 1))
                    filler.CpuPercent = last.CpuPercent
                    filler.MemoryMB = last.MemoryMB
                    filler.RequestsPerSec = 0
                    filler.AvgResponseMs = 0
                    Samples.Insert(0, filler)
                Next
            End If
        End SyncLock
    End Sub

    Private Shared Function GetCpuFallback() As Double
        Try
            Using proc As Process = Process.GetCurrentProcess()
                Dim now As DateTime = DateTime.Now
                Dim cpuTime As TimeSpan = proc.TotalProcessorTime

                If Not _hasBaseline Then
                    _lastCpuTime = cpuTime
                    _lastFallbackSampleTime = now
                    _hasBaseline = True
                    Return 0
                End If

                Dim elapsed As TimeSpan = now - _lastFallbackSampleTime
                Dim cpuDelta As TimeSpan = cpuTime - _lastCpuTime
                _lastCpuTime = cpuTime
                _lastFallbackSampleTime = now

                Dim cpuMs As Double = cpuDelta.TotalMilliseconds
                Dim elapsedMs As Double = elapsed.TotalMilliseconds * Environment.ProcessorCount
                If elapsedMs > 0 Then
                    Return Math.Min(100, (cpuMs / elapsedMs) * 100)
                End If
            End Using
        Catch
        End Try
        Return 0
    End Function

    Public Shared Function GetRecentValues(metric As String, count As Integer) As List(Of Double)
        Dim result As New List(Of Double)()
        SyncLock _sync
            Dim startAt As Integer = Math.Max(0, Samples.Count - count)
            For i As Integer = startAt To Samples.Count - 1
                Dim s As PerformanceSample = Samples(i)
                Select Case metric
                    Case "cpu"
                        result.Add(s.CpuPercent)
                    Case "memory"
                        result.Add(s.MemoryMB)
                    Case "req"
                        result.Add(s.RequestsPerSec)
                    Case "resp"
                        result.Add(s.AvgResponseMs)
                End Select
            Next
        End SyncLock
        Return result
    End Function
End Class