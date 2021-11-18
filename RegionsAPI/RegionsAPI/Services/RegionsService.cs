using Microsoft.Extensions.Caching.Memory;
using Region.Data.Contexts;
using Region.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RegionsAPI.Services
{
    public class RegionsService : IRegionsService
    {

        private readonly PhoneNumbersDbContext _phoneNumbersDbContext;
        private readonly IMemoryCache _memoryCache;

        public RegionsService(PhoneNumbersDbContext phoneNumbersDbContext, IMemoryCache memoryCache)
        {
            _phoneNumbersDbContext = phoneNumbersDbContext;
            _memoryCache = memoryCache;
        }

        public string FindRegion(string phoneNumber)
        {
            var regionName= string.Empty;

            // Check if cache contains regions
            var cacheKey = "regionList";
            if (!_memoryCache.TryGetValue(cacheKey, out List<Regions> allRegions))
            {
                // Load all regions
                allRegions = _phoneNumbersDbContext.Regions.ToList();

                // Setup cache options
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = System.DateTimeOffset.Now.AddMinutes(10),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = System.TimeSpan.FromMinutes(5)
                };
                // set cache
                _memoryCache.Set(cacheKey, allRegions, cacheExpiryOptions);
            }

            // Find region
            // Loop through regions to find code which matches start of phone number
            foreach(var region in allRegions)
            {
                if (phoneNumber.StartsWith(region.RegionCode))
                {
                    regionName = region.RegionName;
                }
            }

            return regionName?.Trim();
        }
    }
}
