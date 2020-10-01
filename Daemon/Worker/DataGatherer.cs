using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Daemon.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Daemon.Worker
{
    internal class DataGatherer : IHostedService
    {
        private readonly IOptionsSnapshot<ApplicationSettings> _applicationSettings;
        private readonly IStorage _storage;

        public DataGatherer(IOptionsSnapshot<ApplicationSettings> applicationSettings, IStorage storage)
        {
            _applicationSettings = applicationSettings;
            _storage = storage;
        }

        void StartDataGathering() =>
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var processDatas = ProcessReceiver.Get().ToArray();
                    _storage.Set(processDatas);
                    await Task.Delay(TimeSpan.FromSeconds(_applicationSettings.Value.IntervalInSeconds));
                }
            });
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartDataGathering();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}