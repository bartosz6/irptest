using System.Threading.Tasks;
using JKang.IpcServiceFramework.Client;

namespace Api.Service
{
    public class ProcStatService : IProcStatService
    {
        private readonly IIpcClientFactory<IInterProcessService> _factory;

        public ProcStatService(IIpcClientFactory<IInterProcessService> factory) => _factory = factory;

        public async Task<ProcStat> GetAll()
        {
            var stats = await _factory.CreateClient("api").InvokeAsync(a => a.Get());
            return ProcStatFactory.Create(stats);
        }
    }
}