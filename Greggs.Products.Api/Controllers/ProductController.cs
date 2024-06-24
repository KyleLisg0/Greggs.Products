using System.Collections.Generic;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Threading.Tasks;
using Greggs.Products.Api.Queries;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<ProductController> _logger;

    private const string DEFAULT_CURRENCY = "GBP";

    public ProductController(
        ILogger<ProductController> logger, 
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<ConvertedProduct>> Get(int pageStart = 0, int pageSize = 5, string currency = DEFAULT_CURRENCY)
    {
        _logger.LogInformation($"Requested start:{pageSize}, size:{pageSize}");

        return await _mediator.Send(new GetAllProductsQuery(pageStart, pageSize, currency));
    }
}