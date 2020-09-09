using AuthorTest.Areas.Identity.Data;
using AuthorTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBreadcrumbs.Attributes;
using System.Diagnostics;

namespace AuthorTest.Controllers
{ // Handles all the logic of views not specific to a role.
    public class HomeController : Controller
    {

        private readonly UserManager<AuthorTestUser> _userManager;
        private readonly SignInManager<AuthorTestUser> signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<AuthorTestUser> userManager, SignInManager<AuthorTestUser> signInManager)
        {
            _userManager = userManager;
            this.signInManager = signInManager;
            _logger = logger;
        }


        [DefaultBreadcrumb("My home")]
        public IActionResult Index()
        {
            if (TempData["submission"] != null)
            {
                ViewBag.submission = TempData["submission"].ToString();
                ViewBag.AlertType = TempData["AlertType"].ToString();

                return View();
            }

            return View();
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
