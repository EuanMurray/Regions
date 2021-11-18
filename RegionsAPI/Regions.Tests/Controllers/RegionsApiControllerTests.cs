using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Region.Data.Contexts;
using RegionsAPI.Controllers;
using RegionsAPI.Services;
using System.Linq;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Region.Tests.Controllers
{
    public class RegionsApiControllerTests : RegionsTestsBase
    {
        public RegionsApiControllerTests()
             : base(new DbContextOptionsBuilder<PhoneNumbersDbContext>()
                  .UseSqlServer("Server=euan-sql-server.database.windows.net;Database=phone_number_information; User ID=EuanMurray;Password=kjg7dsadsahkhjhGGF;Trusted_Connection=False;Encrypt=True;")
                  .Options)
        {
        }

        public RegionsApiController _regionsApiController;

        [SetUp]
        public void Setup()
        {
            // Setup db context and regions cache
            var dbContext = new PhoneNumbersDbContext(ContextOptions);
            var allRegions = dbContext.Regions.ToList();

            var mockMemoryCache = MockMemoryCacheService.GetMemoryCache(allRegions);

            var mockLogger = new Mock<ILogger<RegionsApiController>>();
            //mockLogger.Setup(x => x.LogTrace(It.IsAny<string>())).

            _regionService = new RegionsService(dbContext, mockMemoryCache);
            _regionsApiController = new RegionsApiController(mockLogger.Object, _regionService);
        }

        // Several test cases. All phone numbers start with a Region Code and a random string of numbers
        [Test]
        [TestCase("00943562268767998798273987346762398169536", "Region1")]
        [TestCase("0016980805242344324234123", "Region169")]
        [TestCase("0014051739562123421242141", "Region578")]
        [TestCase("0013847696800003958375", "Region671")]
        [TestCase("00764117482125333333333333333336222222222222", "Region742")]
        [TestCase("0011857064949872198479125789", "Region1001")]
        [TestCase("00337494361876218365812586", "Region1288")]
        [TestCase("00780254832786327890328483729476", "Region2597")]
        [TestCase("007679172417964871236878613587329", "Region3402")]
        [TestCase("0010670876478761248761628435715", "Region4109")]
        public void DetectCountryForTest(string inputPhone, string expectedRegion)
        {
            var regionNameActionResult = _regionsApiController.DetectCountryFor(inputPhone);

            Assert.IsNotNull(regionNameActionResult);

            var okResult = regionNameActionResult as OkObjectResult;
            var result = okResult?.Value as string;

            Assert.AreEqual(result, expectedRegion);
        }
    }
}