using Allup.Services;
using Rent_a_Car_.Net.CustomTokenProviders;
using Rent_a_Car_.Net.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Rent_a_Car_.Net
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<AppDbContext>();

            services.AddControllersWithViews();
            services.ConfigureApplicationCookie(options => {

                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        var requestPath = ctx.Request.Path;
                        if (requestPath.Value == "/Account/Login")
                        {
                            ctx.Response.Redirect("/Auth/Login");
                        }

                        return Task.CompletedTask;
                    }
                };

            });
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentityServices();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
             opt.TokenLifespan = TimeSpan.FromHours(2));
            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(1));
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(opt =>
            {
                opt.LoginPath = "/auth/GoogleLogin";
            })
            .AddGoogle(opt =>
            {
                opt.ClientId = "346849385340-d32e5fbidashvj8u2gq1it88q00g082j.apps.googleusercontent.com";
                opt.ClientSecret = "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";

                //opt.ClientId = "1008467426477 - ngtge4890oarntjundbs923vqql51q4o.apps.googleusercontent.com";
                //opt.ClientSecret = "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";
            })
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(

               name: "areas",
               pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                       "default",
                       "{controller=home}/{action=index}/{id?}"
                       );

            });
        }
    }
}
