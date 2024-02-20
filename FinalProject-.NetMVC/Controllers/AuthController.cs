using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rent_a_Car_.Net.Models;
using System;
using System.Threading.Tasks;
//using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Allup.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Rent_a_Car_.Net.Services;
using System.IO;
using Microsoft.AspNetCore.Authentication;

namespace Rent_a_Car_.Net.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;
        private readonly IWebHostEnvironment _env;

        public AuthController(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IConfiguration config, ILogger<AuthController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _config = config;
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();

            User user = new User
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
                UserName = registerVM.Username,
                PhoneNumber = registerVM.PhoneNumber

            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerVM);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = user.Email }, Request.Scheme);

            EmailService emailService = new EmailService(_config.GetSection("ConfirmationParams:Email").Value, _config.GetSection("ConfirmationParams:Password").Value);

            string url = $"<a href='{confirmationLink}'>linkə buraya klikləyin</a>";

            var emailResult = emailService.SendEmailConfirmation(user.Email, "Hot Wheel | Car Rental - E-poçt təsdiqi",
                        "Zəhmət olmasa e-poçtunuzu təsdiq etmək üçün:" + " " + url);


            await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());

            

            return RedirectToAction(nameof(SuccessRegistration));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [HttpGet]
        public IActionResult SuccessRegistration()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string ReturnUrl)
        {
            if (!ModelState.IsValid) return View();

            User appUser = await _userManager.FindByEmailAsync(loginVM.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Error occured");
                return View(loginVM);
            }



            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("Error", "Bu hesab bloklanib");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Elektron poçt ünvanınızı təsdiq edin");
                return View(loginVM);
            }

            var roles = await _userManager.GetRolesAsync(appUser);



            if (ReturnUrl != null)
            {
                foreach (var item in roles)
                {
                    if (item == "Admin")
                    {
                        return RedirectToAction("index", "dashboard", new { area = "Admin" });
                    }
                    else
                    {
                        if (ReturnUrl == "register")
                        {
                            return RedirectToAction("index", "home");
                        }
                        return Redirect(ReturnUrl); ;
                    }
                }
            }

            if (loginVM.RememberMe==true)
            {
                
            }


            return RedirectToAction("index", "home");
        }


        public IActionResult GoogleLogin(string ReturnUrl)
        {
            string redirectUrl = Url.Action("ExternalResponse", new { ReturnUrl = ReturnUrl });

            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            
            return new ChallengeResult("Google", properties);
        }

        public async Task<IActionResult> ExternalResponse(string ReturnUrl = "/")
        {
            ExternalLoginInfo loginInfo = await _signInManager.GetExternalLoginInfoAsync();
           
            if (loginInfo == null)
                return RedirectToAction("Login");
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
                
                if (loginResult.Succeeded)
                    return Redirect(ReturnUrl);
                else
                {
                    
                    User user = new User
                    {
                        Email = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = loginInfo.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        Name = loginInfo.Principal.FindFirst(ClaimTypes.Name).Value.Split(" ")[0],
                        Surname = loginInfo.Principal.FindFirst(ClaimTypes.Surname).Value,
                        EmailConfirmed = true

                    };
                    
                    IdentityResult createResult = await _userManager.CreateAsync(user);
                    
                    if (createResult.Succeeded)
                    {
                        
                        IdentityResult addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
                       
                        if (addLoginResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, true);
                            
                            return Redirect(ReturnUrl);
                        }
                    }

                }
            }
            return Redirect(ReturnUrl);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action(nameof(ResetPassword), "Auth",
                        new { email = forgotPassword.Email, token = token }, Request.Scheme);

                    _logger.Log(LogLevel.Warning, passwordResetLink);

                    string url = $"<a href='{passwordResetLink}'>Linkə keçid edin</a>";

                    EmailService emailService = new EmailService(_config.GetSection("ConfirmationParams:Email").Value, _config.GetSection("ConfirmationParams:Password").Value);

                    var result = emailService.SendEmailConfirmation(user.Email, "Hot Wheel | Car Rental - şifrə bərpası",
                        "Zəhmət olmasa şifrənizi bərpa etmək üçün" + " " + url);
                    
                    return View("ForgotPasswordConfirmation");
                }

                ModelState.AddModelError("Error","Belə istifadəçi mövcud deyil");
                return View("forgotPassword");
            }
            return View(forgotPassword);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Xəta baş verdi");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        EmailService emailService = new EmailService(_config.GetSection("ConfirmationParams:Email").Value, _config.GetSection("ConfirmationParams:Password").Value);
                        var emailResult = emailService.SendEmailConfirmation(user.Email, "Hot Wheel | Car Rental - şifrə bərpası", "Şifəriniz uğurla yeniləndi");
                        return View("ResetPasswordConfirmation");

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            };
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(string id, string error)
        {
            var user = await _context.Users.FindAsync(id);

            if (user==null)
            {
                ViewBag.ErrorMessage = $"{id}'li istifadəçi tapılmadı";
                return View("NotFound");
            }

            if (error != null)
            {
                ModelState.AddModelError("Error", error);
                return View(user);
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile([FromForm]User user)
        {
            User dbUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (dbUser == null)
            {
                ModelState.AddModelError("Error", "İstifadəçi tapılmadı");
                return View("editprofile");
            }

            if (ModelState["Image"] != null)
            {
                if (!user.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "Image Format Is Wrong");
                    return View();
                }
                if (user.Image.ValidSize(8000))
                {
                    ModelState.AddModelError("Image", "Image Is Oversize");
                    return View();
                }

                if (dbUser.ImageUrl!=null)
                {
                    string path = Path.Combine(_env.WebRootPath, @"assets\images\user", dbUser.ImageUrl);
                    ImageService.DeleteImage(path);
                }
                dbUser.ImageUrl = user.Image.SaveImage(_env, @"assets\images\user");
            }

            var existUserName = _context.Users.FirstOrDefault(x => x.Name.ToLower() == user.Name.ToLower());

            if (existUserName != null)
            {
                if (dbUser.Name.ToLower() != existUserName.Name.ToLower())
                {
                    ModelState.AddModelError("Name", "Bu istifadəçi artıq mövcuddur");
                    return RedirectToAction("editprofile", "auth");
                }

                //mail
                if (existUserName.Email != user.Email)
                {

                    if (dbUser.Email.ToLower() != existUserName.Email.ToLower())
                    {
                        ModelState.AddModelError("Email", "Bu elektron poçt artıq mövcuddur");
                        return RedirectToAction("editprofile", "auth");
                    }

                    bool existEmail = _context.Users.Any(x => x.Email.ToLower() == user.Email.ToLower());

                    if (existEmail)
                    {
                        ModelState.AddModelError("Email", "Bu elektron poçt artıq mövcuddur");
                        var error = "Bu elektron poçt artıq mövcuddur";
                        return RedirectToAction("editprofile", "auth", new { error = error });
                    }
                }

            }

            dbUser.Name = user.Name;
            dbUser.Surname = user.Surname;
            //dbUser.UserName = user.UserName;
            //dbUser.NormalizedUserName = user.UserName.ToUpper();
            dbUser.Email = user.Email;
            dbUser.NormalizedEmail = user.Email.ToUpper();
            dbUser.PhoneNumber = user.PhoneNumber;

            await _context.SaveChangesAsync();

            return RedirectToAction("index", "home");
        }

    }
}
