using AutoMapper;
using smartStoreApi.Common;
using smartStoreApi.Models.Request;
using smartStoreApi.Models.Response;
using smartStoreApi.Properties;
using smartStoreApi.Repositories.Interfaces;
using smartStoreApi.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace smartStoreApi.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IAuthenticateService _authenticateService;
        private readonly IMapper _mapper;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;

        public LoginService(ILoginRepository loginRepository,
                            IAuthenticateService authenticateService,
                            IMapper mapper,
                            IPasswordHashService passwordHashService,
                            IMailService mailService,
                            IUserService userService)
        {
            _loginRepository = loginRepository;
            _authenticateService = authenticateService;
            _mapper = mapper;
            _passwordHashService = passwordHashService;
            _mailService = mailService;
            _userService = userService;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest)
        {
            var userResponse = await _userService.GetUserByEmailAsync(loginRequest.Email);
            if (userResponse == null)
            {
                return null;
            }
            var loginResponse = new LoginResponse { Token = _authenticateService.GenerateSecurityToken(loginRequest.Email, userResponse.Id) };
            return _mapper.Map(userResponse, loginResponse);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(string email)
        {
            var userResponse = await _userService.GetUserByEmailAsync(email);
            if (userResponse == null)
            {
                throw new ArgumentException(string.Format(ValidationMessages.NotFound, ValidationMessages.User));
            }
            Random rnd = new Random();
            int otp = rnd.Next(1000, 9999);
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = email,
                Subject = AppConstants.ForgotPasswordEmailSubject,
                Body = string.Format(AppConstants.ForgotPasswordEmailBody, otp)
            };
            await _mailService.SendEmailAsync(mailRequest);
            return new ForgotPasswordResponse { OTP = otp, ExpirationMinutes = AppConstants.OTPExpirationMinutes };
        }

        public async Task<bool> ResetPasswordAsync(ResetUserPasswordRequest resetUserPasswordRequest)
        {
            var userResponse = await _userService.GetUserByEmailAsync(resetUserPasswordRequest.Email);
            if (userResponse == null)
            {
                throw new ArgumentException(string.Format(ValidationMessages.NotFound, ValidationMessages.User));
            }
            var changePasswordResponse = await _loginRepository.UpdatePasswordAsync(userResponse.Id, _passwordHashService.Hash(resetUserPasswordRequest.Password));
            return changePasswordResponse > 0;
        }
    }
}