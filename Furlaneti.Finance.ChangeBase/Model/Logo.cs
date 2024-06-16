using System.Text.Json.Serialization;

namespace Furlaneti.Finance.ChangeBase.Model
{
    public class Logo
    {
        [JsonPropertyName("small")]
        public string Small { get; set; }

        [JsonPropertyName("big")]
        public string Big { get; set; }
    }
}
