using System;

namespace Api.Service
{
    public readonly struct ProcessData
    {
        public ProcessData(string name, int pid, int sessionId, float cpuUsage, float memoryUsage, float ioDataBytesPerSec, DateTimeOffset date)
        {
            Name = name;
            Pid = pid;
            SessionId = sessionId;
            CpuUsage = cpuUsage;
            MemoryUsage = memoryUsage;
            IODataBytesPerSec = ioDataBytesPerSec;
            Date = date;
        }

        public string Name { get; }
        public int Pid { get; }
        public int SessionId { get; }
        public float CpuUsage { get; }
        public float MemoryUsage { get; }
        public float IODataBytesPerSec { get; }
        public DateTimeOffset Date { get; }
    }
}