using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.Models;
using Greggs.Products.UnitTests.Fixtures;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greggs.Products.UnitTests.StepDefinitions
{
    [Binding]
    public class GetAllProductsStepDefinitions
    {
        private ProductController _controller;

        private string _currencyCodeRequested;
        private IEnumerable<ConvertedProduct> _response;

        private List<Exception> _exceptions = new List<Exception>();

        private Dictionary<string, decimal> _currencyConversions = new Dictionary<string, decimal>
        {
            { "GBP", 1 },
            { "EUR", 1.11M }
        };

        [Given("a previously implemented data access layer")]
        public void GivenAPreviouslyImplementedDataAccessLayer()
        {
            _controller = TestSetup.ProductController_Working(_currencyConversions);
        }

        [Given("a faulty data access layer")]
        public void GivenAFaultyDataAccessLayer()
        {
            _controller = TestSetup.ProductController_DataAccess_Error(_currencyConversions);
        }

        [Given("an exchange rate of {int}GBP to {float} {string}")]
        public void GivenAnExchangeRateOfGBPToEUR(int gbp, Decimal conversionRate, string currency)
        {
            _currencyConversions = new Dictionary<string, decimal>
            {
                { "GBP", 1 },
                { currency, conversionRate }
            };

            _currencyCodeRequested = currency;

            _controller = TestSetup.ProductController_Working(_currencyConversions);
        }

        [When("I hit a specified endpoint to get a list of products")]
        public async Task WhenIHitASpecifiedEndpointToGetAListOfProducts()
        {
            try
            {
                _response = await _controller.Get();
            }
            catch(Exception ex)
            {
                _exceptions.Add(ex);
            }
        }

        [Then("a list of products is returned that uses the data access implementation rather than the static list it current utilises")]
        public void ThenAListOfProductsIsReturnedThatUsesTheDataAccessImplementationRatherThanTheStaticListItCurrentUtilises()
        {
            _response.Count().Should().Be(5);
        }

        [Then("an exception is thrown")]
        public void ThenAnExceptionIsThrown()
        {
            _exceptions.Should().NotBeEmpty();
        }

        [Then("I will get the products and their prices returned")]
        public void ThenIWillGetTheProductsAndTheirPriceSReturned()
        {
            _response.Count().Should().Be(5);
            _response.All(p => p.CurrencyCode == _currencyCodeRequested);
            _response.All(p => p.ConvertedPrice > 0);
        }

    }
}
