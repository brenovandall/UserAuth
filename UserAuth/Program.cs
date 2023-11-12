using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserAuth.Authorization;
using UserAuth.Data;
using UserAuth.Models;
using UserAuth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// database connetion -- > AppConnection == the own connection string 
var connection = builder.Configuration.
    GetConnectionString("AppConnection");

// mysql connection -- >
builder.Services.AddDbContext<ApplicationContext>(opts => opts.UseMySql(
    builder.Configuration.GetConnectionString("AppConnection"),
    ServerVersion.AutoDetect(connection)));

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders(); // token provider with identity on -- > Microsoft.AspNetCore.Identity v6

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // using auto mapper

builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorization>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("minage", policy => policy.AddRequirements(new MinAge(18)));
});

builder.Services.AddScoped<UserService>(); // scoped dependency injection

builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
