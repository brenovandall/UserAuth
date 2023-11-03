using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserAuth.Data.Dtos;
using UserAuth.Models;

namespace UserAuth.Services
{
    public class signService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;

        public signService(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task Sign(UserDto user)
        {
            User users = _mapper.Map<User>(user);

            IdentityResult result = await _userManager.CreateAsync(users, user.Password);

            if (!result.Succeeded) 
                throw new ApplicationException("Failed signing user!");


        }
    }
}
