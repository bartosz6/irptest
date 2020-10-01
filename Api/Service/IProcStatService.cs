using System.Threading.Tasks;

namespace Api.Service
{
    public interface IProcStatService
    {
        Task<ProcStat> GetAll();
    }
}