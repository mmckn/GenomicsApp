using AuthorTest.Areas.Identity.Data;
using AuthorTest.Data;
using AuthorTest.Models;
using AuthorTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorTest.Controllers
{
    // Handles all the logic to do with user registration, login and logout.
    public class UserAccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        //UserManager is a class that is used to manage the users in the Identity database
        private readonly UserManager<AuthorTestUser> userManager;
        private readonly AuthorDbContext authorDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SignInManager<AuthorTestUser> signInManager;
        private readonly IGenomeService gsv;

        public UserAccountController(IGenomeService genomeService, RoleManager<IdentityRole> roleManager, UserManager<AuthorTestUser> userManager, AuthorDbContext authorDbContext, IHttpContextAccessor httpContextAccessor, SignInManager<AuthorTestUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.authorDbContext = authorDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.signInManager = signInManager;
            gsv = genomeService;
        }


        [HttpGet]
        public IActionResult UserRegistration()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UserRegistration(RegisterViewModel input)
        {

            if (ModelState.IsValid)
            {
                var result = await gsv.RegisterUser(input);

                if (result != null)
                {
                    await signInManager.SignInAsync(result, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                ViewBag.submission = "There is already a user with this email registered!";
                ViewBag.AlertType = "alert-danger";
                return View();
            }
            return View();
        }


        [HttpGet]
        public IActionResult ResearcherRegistration()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResearcherRegistration(ResearcherRegisterViewModel input)
        {
            if (ModelState.IsValid)
            {
           
                var result = await gsv.RegisterResearcher(input);
                if (result != null)
                {
                    await signInManager.SignInAsync(result, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                ViewBag.submission = "There is already a user with this email registered!";
                ViewBag.AlertType = "alert-danger";
                return View();
            }
            return View();
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SignIn(SignInViewModel input)
        {
            if (ModelState.IsValid)
            {
                var result = gsv.SignInUser(input);

                if (result.Result)
                {
                    return RedirectToAction("index", "home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Details, please try again");

            return View(input);
        }


        [HttpGet]
        public IActionResult SignOut()
        {
            gsv.SignOutUser();

            return RedirectToAction("index", "home");
        }
    }
}
