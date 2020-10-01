using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Daemon.Service;

namespace Daemon.Infrastructure
{
    public class InMemoryStaticStorage : IStorage
    {
        private static ReadOnlyCollection<ProcessData> _inmemoryStore = 
            new ReadOnlyCollection<ProcessData>(new List<ProcessData>(0));
        public void Set(ProcessData[] data) => _inmemoryStore = new ReadOnlyCollection<ProcessData>(data);
        public ProcessData[] Get() => _inmemoryStore.ToArray();
    }
}