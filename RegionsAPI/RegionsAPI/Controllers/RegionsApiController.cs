using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegionsAPI.Services;
using System;

namespace RegionsAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RegionsApiController : ControllerBase
    {
        private readonly ILogger<RegionsApiController> _logger;
        private readonly IRegionsService _regionService;

        public RegionsApiController(ILogger<RegionsApiController> logger, IRegionsService regionService)
        {
            _logger = logger;
            _regionService = regionService;
        }

        [HttpGet]
	    public IActionResult DetectCountryFor([FromQuery] string phone)
        {
            try
            {
                if (string.IsNullOrEmpty(phone)) return BadRequest();

                _logger.LogTrace($"Searching Regions. Phone: {phone}");
                var region = _regionService.FindRegion(phone);

                if (string.IsNullOrEmpty(region)) return NotFound();

                _logger.LogTrace("Region Found");
                return Ok(region);

            } catch (Exception e)
            {
                _logger.LogError($"error retrieving region. Phone: {phone} Exception: {e}");
                return BadRequest();
            }
        }
    }
}
