using System.Linq;

namespace Api.Service
{
    public static class ProcStatFactory
    {
        public static ProcStat Create(ProcessData[] stats) =>
            new ProcStat
            {
                Data = stats.Select(MapItem).OrderBy(a => a.Pid).ToArray(),
                Average = CalculateAverage(stats)
            };

        private static ProcStat.Summary CalculateAverage(ProcessData[] stats)
        {
            if (stats.Any() is false)
                return new ProcStat.Summary();
            
            var maxIoUsage = stats.OrderByDescending(s => s.IODataBytesPerSec).FirstOrDefault();
            return new ProcStat.Summary
            {
                AverageCpuUsage = stats.Average(s => s.CpuUsage),
                TotalMemoryUsageInMb = stats.Sum(s => s.MemoryUsage / 1024 / 1024),
                MaxIoUsage = maxIoUsage.IODataBytesPerSec,
                MaxIoUsagePid = maxIoUsage.Pid
            };
        }

        private static ProcStat.Item MapItem(ProcessData arg) =>
            new ProcStat.Item
            {
                Name = arg.Name,
                Pid = arg.Pid,
                SessionId = arg.SessionId,
                PercentageCpuUse = arg.CpuUsage,
                MemoryUsageKB = arg.MemoryUsage / 1024,
                IODataBytesPerSec = arg.IODataBytesPerSec
            };
    }
}