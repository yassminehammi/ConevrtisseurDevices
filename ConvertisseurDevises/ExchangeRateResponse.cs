using System.Text.Json.Serialization;

public class ExchangeRateResponse
{
    [JsonPropertyName("conversion_rates")]
    public Dictionary<string, double> ConversionRates { get; set; }
}