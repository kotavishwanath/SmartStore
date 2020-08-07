namespace smartStoreApi.Common
{
    public static class AppConstants
    {
        public const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
        public const string ForgotPasswordEmailBody = "Your Smart Store Verification Code is {0}";
        public const string ForgotPasswordEmailSubject = "Smart Store Verification Code";
        public const int OTPExpirationMinutes = 15;
        public const string AuthorizationHeader = "Authorization";
    }
}