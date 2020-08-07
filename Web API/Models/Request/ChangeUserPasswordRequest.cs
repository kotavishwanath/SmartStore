using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace smartStoreApi.Models.Request
{
    public class ChangeUserPasswordRequest
    {
        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }

        [JsonProperty("newPassword")]
        [Required]
        public string NewPassword { get; set; }
    }
}