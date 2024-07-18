using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;

            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var List = _userManager.Users.ToList();
            return View(List);
        }

        //Get : Open Register Form
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel registerUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser RUM = new ApplicationUser()
                {
                    Id = registerUser.Id,
                    UserName = registerUser.UserName,
                    Email = registerUser.Email,
                    FName = registerUser.FName,
                    LName = registerUser.LName,

                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    DisplayName = registerUser.Email.Split("@")[0],
                    //PasswordHash = registerUser.Password,


                };


                IdentityResult result = await _userManager.CreateAsync(RUM, registerUser.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(RUM, false);
                    return RedirectToAction(nameof(Login));
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View(registerUser);
        }

        public IActionResult LogOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveLogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserModel loginUserModel)
        {
            if (ModelState.IsValid)
            {

                var username = await _userManager.FindByNameAsync(loginUserModel.UserName);

                if (username != null)
                {

                    bool Check = await _userManager.CheckPasswordAsync(username, loginUserModel.Password);

                    if (Check == true)
                    {
                        var result = await _signInManager.PasswordSignInAsync(username, loginUserModel.Password, loginUserModel.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid Password");
                    }
                }
                else
                    ModelState.AddModelError("UserName", "UserName Not Found");


            }

            return View(loginUserModel);


        }



    }
}
