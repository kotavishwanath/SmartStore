namespace smartStoreApi.Common
{
    public static class Routes
    {
        public const string GetUser = "GetUser/{email}";
        public const string UserControllerRoute = "user";
        public const string LoginControllerRoute = "login";
        public const string ChangePassword = "changepassword";
        public const string ResetPassword = "resetpassword";
        public const string ForgotPassword = "forgotpassword/{email}";
        public const string SaveUser = "saveuser";
    }
}