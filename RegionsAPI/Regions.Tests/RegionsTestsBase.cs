using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Region.Data.Contexts;
using RegionsAPI.Services;

namespace Region.Tests
{
    public class RegionsTestsBase
    {
        public RegionsTestsBase(DbContextOptions<PhoneNumbersDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }

        public RegionsService? _regionService;
        protected DbContextOptions<PhoneNumbersDbContext> ContextOptions { get; }

        public static class MockMemoryCacheService
        {
            public static IMemoryCache GetMemoryCache(object expectedValue)
            {
                var mockMemoryCache = new Mock<IMemoryCache>();
                mockMemoryCache
                    .Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedValue))
                    .Returns(true);
                return mockMemoryCache.Object;
            }
        }
    }
}
