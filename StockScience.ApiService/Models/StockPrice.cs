using System.Globalization;

namespace StockScience.PriceApi.Models
{
    public record StockPrice(string Symbol, int Price, DateTime DateTime)
    {
        public override string ToString()
        {
            return $"{Symbol}: ${Price} at ${DateTime.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture)}";
        }
    }
}
