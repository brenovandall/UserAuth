using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UserAuth.Authorization;

public class AgeAuthorization : AuthorizationHandler<MinAge>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        MinAge requirement)
    {
        var dateClaim = context.User.FindFirst(x => x.Type == ClaimTypes.DateOfBirth);

        if (dateClaim == null)
        {
            return Task.CompletedTask;
        }

        var date = Convert.ToDateTime(dateClaim.Value);

        var userAge = DateTime.Today.Year - date.Year;

        if (date > DateTime.Today.AddYears(userAge))
        {
            userAge--;
        }

        if (userAge >= requirement.Age)
        {
            context.Succeed(requirement);
        }
        //context.Succeed(requirement); -- > testing success
        return Task.CompletedTask;
    }
}
