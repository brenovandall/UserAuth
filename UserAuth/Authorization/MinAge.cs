using Microsoft.AspNetCore.Authorization;

namespace UserAuth.Authorization;

public class MinAge : IAuthorizationRequirement // inheritance from Authorization interface
{
    public MinAge(int age)
    {
        Age = age;
    }
    public int Age { get; set; } // just the user age for authentication
    // if user has less than legal age, user's not allow to access the application!! 
}
