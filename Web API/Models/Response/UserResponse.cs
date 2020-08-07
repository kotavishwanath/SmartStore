using Newtonsoft.Json;

namespace smartStoreApi.Models.Response
{
    public class UserResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonIgnore]
        [JsonProperty("password")]
        public string Password { get; set; }

    }
}