using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Daemon.Service;

namespace Daemon.Infrastructure
{
    public static class ProcessReceiver
    {
        public static IEnumerable<ProcessData> Get() => Process.GetProcesses().Select(Get);

        private static readonly ConcurrentDictionary<string, PerformanceCounter> Counters = new ConcurrentDictionary<string, PerformanceCounter>();

        public static ProcessData Get(Process process) =>
            new ProcessData(process.ProcessName,
                process.Id,
                process.SessionId,
                GetCpuUsage(process) / Environment.ProcessorCount,
                GetMemoryUsage(process),
                GetIoDataBytesPerSec(process),
                DateTimeOffset.UtcNow);

        private static Func<float> GetCounter(string categoryName, string counterName, string instanceName) =>
            Counters.GetOrAdd(
                    $"{instanceName}{counterName}",
                    _ => new PerformanceCounter(categoryName, counterName, instanceName, true))
                .NextValue;

        private static float GetIoDataBytesPerSec(Process process) =>
            GetCounter("Process", "IO Data Bytes/sec", process.ProcessName)();

        private static float GetMemoryUsage(Process process) =>
            GetCounter("Process", "Working Set", process.ProcessName)();

        private static float GetCpuUsage(Process process) =>
            GetCounter("Process", "% Processor Time", process.ProcessName)();
    }
}