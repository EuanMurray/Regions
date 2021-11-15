using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegionsAPI.Services;
using System;

namespace RegionsAPI.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly ILogger<RegionsController> _logger;
        private readonly IRegionsService _regionService;

        public RegionsController(ILogger<RegionsController> logger, IRegionsService regionService)
        {
            _logger = logger;
            _regionService = regionService;
        }

        [HttpGet("DetectCountryFor")]
	    IActionResult DetectCountryFor([FromQuery] string phone)
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
