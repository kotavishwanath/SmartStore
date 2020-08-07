using Newtonsoft.Json;

namespace smartStoreApi.Models.Response
{
    public class LoginResponse : UserResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}