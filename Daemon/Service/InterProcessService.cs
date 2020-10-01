using Daemon.Infrastructure;

namespace Daemon.Service
{
    internal class InterProcessService : IInterProcessService
    {
        private readonly IStorage _storage;

        public InterProcessService(IStorage storage) => _storage = storage;

        public ProcessData[] Get() => _storage.Get();
    }
}