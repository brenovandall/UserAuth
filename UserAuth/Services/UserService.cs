using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

        public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
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

        public async Task Login(UserLoginDto login)
        {
            var response = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false); // create auth by password authorization, the paramethers are (username field, password field, isPersistent = true or false, lockoutOnFailure = true or false) lockoutOnFailure = true or false)
                                                                                                                   // isPersistent == cookie should persist after the browser is closed
                                                                                                                   // lockoutOnFailure == user account should be locked if the sign-in fails

            if (!response.Succeeded) throw new ApplicationException("User not allowed!");
        }
    }
}
