using System.Text.Json.Serialization;

namespace Furlaneti.Finance.ChangeBase.Model
{
    public class MarketTime
    {
        [JsonPropertyName("open")]
        public string Open { get; set; }

        [JsonPropertyName("close")]
        public string Close { get; set; }

        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }
    }
}
