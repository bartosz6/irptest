using Daemon.Service;

namespace Daemon.Infrastructure
{
    public interface IStorage
    {
        void Set(ProcessData[] data);
        ProcessData[] Get();
    }
}