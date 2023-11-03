using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserAuth.Models;

namespace UserAuth.Data;

public class ApplicationContext :  IdentityDbContext<User>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> opts) : base(opts)
    {

    }

}
