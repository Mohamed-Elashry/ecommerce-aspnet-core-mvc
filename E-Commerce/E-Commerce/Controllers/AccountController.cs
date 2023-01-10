using E_Commerce.Data.Static;
using E_Commerce.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _Context;
        private readonly string _lang;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Context = context;
            _lang = Thread.CurrentThread.CurrentUICulture.Name;  
        }
        public async Task<IActionResult> Users() => View(await _Context.Users.ToListAsync());
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            
            var _messageError = _lang == "en-US" ?
                                        "Sorry, wrong Credential. Please try again!" :
                                        "!عفوا، هناك خطا في البيانات، فضلا حاول مرة اخري ";

            if (!ModelState.IsValid)
                return View(loginVM);
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck =await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var managerCheckPassword = await _signInManager.PasswordSignInAsync(user, loginVM.Password,false,false);
                    if (managerCheckPassword.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
            }         
            TempData["ErrorLogin"] = _messageError;
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var _messageError = "";

            if (!ModelState.IsValid)
                return View(registerVM);
            var Exituser = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (Exituser == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FullName = registerVM.FullName,
                    Email = registerVM.EmailAddress, 
                    UserName = registerVM.EmailAddress
                };
                var createResult =  await _userManager.CreateAsync(user, registerVM.Password);
                if (createResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    _messageError = createResult.Errors?.FirstOrDefault()?.Description;
                }
            }
            else
            {
                _messageError = _lang == "en-US" ?
                                        "Sorry, this email address is already in use. Please try again!" :
                                        "!عفوا، هذا  البريد الالكتروني قيد الاستخدام، فضلا حاول مرة اخري ";
            }
            TempData["ErrorLogin"] = _messageError;
            return View(registerVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}
