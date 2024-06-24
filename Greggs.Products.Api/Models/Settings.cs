using System.Collections.Generic;

namespace Greggs.Products.Api.Models
{
    public class Settings
    {
        public Dictionary<string, decimal> CurrencyConversionRates { get; set; }
    }
}
