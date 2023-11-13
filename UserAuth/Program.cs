using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserAuth.Authorization;
using UserAuth.Data;
using UserAuth.Models;
using UserAuth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// database connetion -- > AppConnection == the own connection string 
var connection = builder.Configuration
    ["SConnectionStrings:AppConnection"];

// mysql connection -- >
builder.Services.AddDbContext<ApplicationContext>(opts => opts.UseMySql(
    builder.Configuration.GetConnectionString("AppConnection"),
    ServerVersion.AutoDetect(connection)));

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders(); // token provider with identity on -- > Microsoft.AspNetCore.Identity v6

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // using auto mapper

builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorization>(); // authorization and class authorization instace 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// policy instace, requirement is implemented on creation -- >
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("minage", policy => policy.AddRequirements(new MinAge(18)));
});

// validate all information -- >
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<UserService>(); // scoped dependency injection for user service

builder.Services.AddScoped<TokenService>();// scoped dependency injection for token service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // need to be declared right here to accept the previous auth!
app.UseAuthorization();


app.MapControllers();

app.Run();
