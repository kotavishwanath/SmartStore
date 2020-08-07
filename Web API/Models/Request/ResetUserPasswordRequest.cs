using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace smartStoreApi.Models.Request
{
    public class ResetUserPasswordRequest
    {
        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }
    }
}