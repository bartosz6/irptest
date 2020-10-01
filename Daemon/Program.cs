using System.Collections.Generic;
using Daemon.Infrastructure;
using Daemon.Service;
using Daemon.Worker;
using JKang.IpcServiceFramework.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Daemon
{
    class Program
    {
        private static IConfigurationRoot _configuration =
            new ConfigurationRoot(new List<IConfigurationProvider>(0));
        
        static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddScoped<IInterProcessService, InterProcessService>()
                    .AddHostedService<DataGatherer>()
                    .AddSingleton<IStorage, InMemoryStaticStorage>()
                    .Configure<ApplicationSettings>(_configuration.GetSection("ApplicationSettings")))
                .ConfigureIpcHost(builder => builder.AddNamedPipeEndpoint<IInterProcessService>("ProcStats"))
                .ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Information))
                .ConfigureAppConfiguration(ctx => _configuration = ctx
                    .AddJsonFile("appsettings.json", true, true)
                    .AddEnvironmentVariables()
                    .Build());
    }
}