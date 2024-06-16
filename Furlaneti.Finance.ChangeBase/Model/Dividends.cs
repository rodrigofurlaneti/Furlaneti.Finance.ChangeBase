using System.Text.Json.Serialization;

namespace Furlaneti.Finance.ChangeBase.Model
{
    public class Dividends
    {
        [JsonPropertyName("yield_12m")]
        public double Yield_12m { get; set; }

        [JsonPropertyName("yield_12m_sum")]
        public double Yield_12m_sum { get; set; }
    }
}
