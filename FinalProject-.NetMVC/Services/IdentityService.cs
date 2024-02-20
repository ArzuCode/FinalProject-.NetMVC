using System;
using Rent_a_Car_.Net.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rent_a_Car_.Net.Models;
using Rent_a_Car_.Net.CustomTokenProviders;

namespace Allup.Services
{
    public static class IdentityService
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        
        {
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireDigit = true;

                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders().AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation");


            return services;
        }
    }
}
