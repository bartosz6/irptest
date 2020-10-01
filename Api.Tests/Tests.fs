module Tests

open System
open Api.Service
open Xunit
open FsUnit.Xunit

[<Fact>]
let ``when stats are empty returns default average`` () =
    let procStat = ProcStatFactory.Create([|
        ProcessData()
    |])
    
    procStat.Average.AverageCpuUsage |> should equal 0.0f
    procStat.Average.MaxIoUsage |> should equal 0.0f
    procStat.Average.MaxIoUsagePid |> should equal 0
    procStat.Average.TotalMemoryUsageInMb |> should equal 0.0f
    
[<Fact>]
let ``when stats are not empty but process data is default returns default average`` () =
    let procStat = ProcStatFactory.Create([|
        ProcessData()
    |])
    
    procStat.Average.AverageCpuUsage |> should equal 0.0f
    procStat.Average.MaxIoUsage |> should equal 0.0f
    procStat.Average.MaxIoUsagePid |> should equal 0
    procStat.Average.TotalMemoryUsageInMb |> should equal 0.0f
    
[<Fact>]
let ``when stats are not empty returns summary values`` () =
    let procStat = ProcStatFactory.Create([|
        ProcessData("p1", 1, 1, 20.0f, 50.0f, 10.0f, DateTimeOffset.UtcNow)
        ProcessData("p2", 2, 2, 10.0f, 10.0f, 12.0f, DateTimeOffset.UtcNow)
        ProcessData("p3", 0, 2, 0.0f, 0.0f, 0.0f, DateTimeOffset.UtcNow)
    |])
    
    procStat.Average.AverageCpuUsage |> should equal 10.0f
    procStat.Average.MaxIoUsage |> should equal 12.0f
    procStat.Average.MaxIoUsagePid |> should equal 2
    procStat.Average.TotalMemoryUsageInMb |> should equal (60.0f / 1024.0f / 1024.0f)
    procStat.Data.[0].Pid |> should equal 0