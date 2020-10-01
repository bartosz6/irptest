using System.Threading.Tasks;
using Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcStatController : ControllerBase
    {
        private readonly IProcStatService _service;

        public ProcStatController(IProcStatService service) => _service = service;

        [HttpGet]
        public Task<ProcStat> Get() => _service.GetAll();
    }
}