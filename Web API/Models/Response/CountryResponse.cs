using Newtonsoft.Json;

namespace smartStoreApi.Models.Response
{
    public class CountryResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}