using AutoMapper;
using smartStoreApi.Models.Response;

namespace smartStoreApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() => CreateMap<UserResponse, LoginResponse>();
    }
}