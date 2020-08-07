using Newtonsoft.Json;

namespace smartStoreApi.Models.Response
{
    public class ForgotPasswordResponse
    {
        [JsonProperty("otp")]
        public int OTP { get; set; }

        [JsonProperty("expirationMinutes")]
        public int ExpirationMinutes { get; set; }
    }
}