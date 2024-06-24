using Greggs.Products.Api.Models;
using System.Collections.Generic;

namespace Greggs.Products.Api.Extensions
{
    public interface IProductConverter
    {
        ConvertedProduct ToConvertedProduct(Product product, string currencyCode);
    }

    public class ProductConverter : IProductConverter
    {
        private readonly Dictionary<string, decimal> _conversionRates;

        public ProductConverter(Dictionary<string, decimal> conversionRates)
        {
            _conversionRates = conversionRates;
        }

        public ConvertedProduct ToConvertedProduct(Product product, string currencyCode)
        {
            return new ConvertedProduct(
                product.Name, 
                product.PriceInPounds, 
                _conversionRates[currencyCode], 
                currencyCode);
        }
    }
}
