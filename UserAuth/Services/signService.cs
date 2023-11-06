using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserAuth.Data.Dtos;
using UserAuth.Models;

namespace UserAuth.Services
{
    // using service for encapsulation and good performance
    public class signService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;

        public signService(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        // tasks represent asynchronous operations, enabling concurrent execution without blocking the main thread
        public async Task Sign(UserDto user)
        {
            User users = _mapper.Map<User>(user);

            IdentityResult result = await _userManager.CreateAsync(users, user.Password);

            // doing only the fail instance, cause i cant return a 200 if i have a task
            if (!result.Succeeded) // if dont receive a successfull identity result, system will return an exception
                throw new ApplicationException("Failed signing user!");


        }
    }
}
