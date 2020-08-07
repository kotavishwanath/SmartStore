namespace smartStoreApi.Models.Response
{
    public class AuthenticationResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int MCId { get; set; }
        public string Email { get; set; }
    }
}