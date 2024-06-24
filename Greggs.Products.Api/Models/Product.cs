namespace Greggs.Products.Api.Models;

public class Product
{
    public string Name { get; set; }
    public decimal PriceInPounds { get; set; }
}

public class ConvertedProduct : Product
{
    public decimal Multiplier { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal ConvertedPrice { get; private set; }

    public ConvertedProduct(string name, decimal priceInGBP, decimal multiplier, string currencyCode)
    {
        Name = name;
        PriceInPounds = priceInGBP;
        CurrencyCode = currencyCode;
        Multiplier = multiplier;

        ConvertedPrice = priceInGBP * multiplier;
    }

}