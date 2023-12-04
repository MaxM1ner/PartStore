using DataAccess;
using Entities.Models;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace xUnitTests
{
    public class FeatureManagerTest
    {
        private readonly FeatureManager _featureManager;
        private readonly ITestOutputHelper _outputHelper;

        public FeatureManagerTest(ITestOutputHelper testOutputHelper, FeatureManager featureManager)
        {
            _outputHelper = testOutputHelper;
            _featureManager = featureManager;
        }
        [Fact]
        public async Task GetFetures_Include()
        {
            //List<Feature> a = await _featureManager.GetFeaturesAsync(true, true);
            //foreach (var b in a)
            //{
            //    Assert.NotNull(b.Products);
            //    Assert.NotNull(b.Type);
            //}
            
        }

    }
}
