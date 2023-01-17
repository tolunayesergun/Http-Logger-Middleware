using HttpLogger.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HttpLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleRequestService _sampleRequestService;

        public SampleController(ISampleRequestService sampleRequestService)
        {
            _sampleRequestService = sampleRequestService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sampleRequestService.GetDataFromAPI());
        }
    }
}
