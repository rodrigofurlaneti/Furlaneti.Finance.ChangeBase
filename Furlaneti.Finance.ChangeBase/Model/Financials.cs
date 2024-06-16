using System.Text.Json.Serialization;

namespace Furlaneti.Finance.ChangeBase.Model
{
    public class Financials
    {
        [JsonPropertyName("quota_count")]
        public long Quota_count { get; set; }

        [JsonPropertyName("dividends")]
        public Dividends Dividends { get; set; }
    }
}
