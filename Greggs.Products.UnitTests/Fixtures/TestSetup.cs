using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Extensions;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;

namespace Greggs.Products.UnitTests.Fixtures
{
    internal static class TestSetup
    {
        public static ProductController ProductController_Working(Dictionary<string, decimal> currencyConversions)
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(GetAllProductsHandler).Assembly))
                .AddSingleton<IDataAccess<Product>, ProductAccess>()
                .AddSingleton<IProductConverter>(c => new ProductConverter(currencyConversions))
                .AddLogging()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var logger = serviceProvider.GetRequiredService<ILogger<ProductController>>();

            return new ProductController(logger, mediator);
        }

        public static ProductController ProductController_DataAccess_Error(Dictionary<string, decimal> currencyConversions)
        {
            var mockDataAccess = new Mock<IDataAccess<Product>>();
            mockDataAccess.Setup(d => d.List(It.IsAny<int?>(), It.IsAny<int?>()))
                .Throws(new Exception());

            var serviceProvider = new ServiceCollection()
                .AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(GetAllProductsHandler).Assembly))
                .AddSingleton(x => mockDataAccess.Object)
                .AddSingleton<IProductConverter>(c => new ProductConverter(currencyConversions))
                .AddLogging()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var logger = serviceProvider.GetRequiredService<ILogger<ProductController>>();

            return new ProductController(logger, mediator);
        }
    }
}
