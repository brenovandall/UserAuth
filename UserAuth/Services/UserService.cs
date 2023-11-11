using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserAuth.Data.Dtos;
using UserAuth.Models;

namespace UserAuth.Services
{
    // using service for encapsulation and good performance
    public class UserService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private TokenService _tokenService;

        public UserService(IMapper mapper, UserManager<User> userManager,
            SignInManager<User> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

        public async Task<string> Login(UserLoginDto logindto)
        {
            var response = await _signInManager.PasswordSignInAsync(logindto.Username, logindto.Password, false, false);

            if (!response.Succeeded)
            {
                throw new ApplicationException("user not allowed!");
            }

            var userWithToken = _signInManager.UserManager.Users.First(x => x.NormalizedUserName == logindto.Username);

            var token = _tokenService.GenerateToken(userWithToken);

            return token;
        } 
    }
}
