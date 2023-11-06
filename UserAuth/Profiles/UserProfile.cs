using AutoMapper;
using UserAuth.Data.Dtos;
using UserAuth.Models;

namespace UserAuth.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>(); // using auto mapper by user class data transfer to user normal class 
        }
    }
}
