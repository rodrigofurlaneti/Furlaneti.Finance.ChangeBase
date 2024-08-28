using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace Furlaneti.Finance.ChangeBase.Model
{
    public class Dividends
    {
        //Percentual yield últimos 12 meses
        [JsonPropertyName("yield_12m")]
        public double? Yield_12m { get; set; }

        //Soma últimos 12 meses em reais(R$)
        [JsonPropertyName("yield_12m_sum")]
        public double? Yield_12m_sum { get; set; }
    }
}
