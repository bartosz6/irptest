namespace Api.Service
{
    public class ProcStat
    {
        public class Item
        {
            public string Name { get; set; }
            public int Pid { get;  set;}
            public int SessionId { get;  set;}
            public float PercentageCpuUse { get;  set;}
            public float MemoryUsageKB { get;  set;}
            public float IODataBytesPerSec { get; set; }
        }
        
        public Item[] Data { get; set; }
        public Summary Average { get; set; }

        public class Summary
        {
            public float AverageCpuUsage { get; set; }
            public float TotalMemoryUsageInMb { get; set; }
            public float MaxIoUsage { get; set; }
            public int MaxIoUsagePid { get; set; }
        }
    }
}