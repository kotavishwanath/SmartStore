using smartStoreApi.Models.Request;
using smartStoreApi.Models.Response;
using smartStoreApi.Properties;
using smartStoreApi.Repositories.Interfaces;
using smartStoreApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smartStoreApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticateService _authenticateService;
        private readonly ILoginRepository _loginRepository;
        private readonly IPasswordHashService _passwordHashService;

        public UserService(IUserRepository userRepository, IAuthenticateService authenticateService, ILoginRepository loginRepository, IPasswordHashService passwordHashService)
        {
            _userRepository = userRepository;
            _authenticateService = authenticateService;
            _loginRepository = loginRepository;
            _passwordHashService = passwordHashService;
        }

        public async Task<UserResponse> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> ChangeUserPasswordAsync(ChangeUserPasswordRequest changeUserPasswordRequest)
        {
            var userId = _authenticateService.GetUserId();
            var userResponse = await _userRepository.GetUserByIdAsync(userId);

            var passwordResult = _passwordHashService.Verify(userResponse.Password, changeUserPasswordRequest.Password);
            if (!passwordResult)
            {
                throw new ArgumentException(string.Format(ValidationMessages.Invalid, nameof(changeUserPasswordRequest.Password)));
            }

            var changePasswordResponse = await _loginRepository.UpdatePasswordAsync(userResponse.Id, _passwordHashService.Hash(changeUserPasswordRequest.NewPassword));
            return changePasswordResponse > 0;
        }

        public async Task<UserResponse> GetUserAsync()
        {
            return await _userRepository.GetUserByIdAsync(_authenticateService.GetUserId());
        }

        public async Task<string> SaveUserAsync(UserRequest userRequest)
        {
            if(userRequest == null)
            {
                return "Invalid User details";
            }
            var user = await _userRepository.GetUserByEmailAsync(userRequest.Email);
            if (user != null)
            {
                return "User already exist. Please try with different email";
            }
            return await _userRepository.InsertUserAsync(userRequest) ? "User Registration successful" : "User Regstration Failed";
        }

        public async Task<UserProductResponse> GetUserProductsAsync(int userId)
        {
            return await _userRepository.GetUserProductsAsync(userId);
        }

        public async Task<ProductDetailResponse> GetProductDetailsAsync(int productId, int categoryId, int userId)
        {
            return await _userRepository.GetProductDetailsAsync(productId, categoryId, userId);
        }
            
    }
}