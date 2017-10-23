using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using StackOverflow.Models;
using StackOverflow.ViewModels;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StackOverflow.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        //dependency injection:
        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                  ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
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
        public async Task<IActionResult> Register (RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
			ViewData["ReturnUrl"] = returnUrl;
			if (model.Email.IndexOf('@') > -1)
			{
				//Validate email format
				string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
									   @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
										  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
				Regex re = new Regex(emailRegex);
				if (!re.IsMatch(model.Email))
				{
					ModelState.AddModelError("Email", "Email is not valid");
				}
			}
			else
			{
				//validate Username format
				string emailRegex = @"^[a-zA-Z0-9]*$";
				Regex re = new Regex(emailRegex);
				if (!re.IsMatch(model.Email))
				{
					ModelState.AddModelError("Email", "Username is not valid");
				}
			}


            if(ModelState.IsValid)
            {
                var userName = model.Email;
                if (userName.IndexOf('@') > -1)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View();
                    }
					else
					{
						userName = user.UserName;
					}
                }
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
