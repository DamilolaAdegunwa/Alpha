using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using Alpha.Infrastructure.Data;
using Alpha.Core.Interfaces;
using Alpha.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Alpha.Core.Models.DTOs;

namespace Alpha.Web.Controllers
{
   /// <summary>
   /// A sample MVC controller that uses views.
   /// Razor Pages provides a better way to manage view-based content, since the behavior, viewmodel, and view are all in one place,
   /// rather than spread between 3 different folders in your Web project. Look in /Pages to see examples.
   /// See: https://ardalis.com/aspnet-core-razor-pages-%E2%80%93-worth-checking-out/
   /// </summary>
   public class HomeController : Controller
  {
    private readonly UserManager<IdentityUser> _UserManager;
    private readonly SignInManager<IdentityUser> _SignInManager;
    private readonly AppDbContext _appDbContext;
    private readonly IAuthService _authService;
    public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext appDbContext, IAuthService authService)
    {
      _UserManager = userManager;
      _SignInManager = signInManager;
      _appDbContext = appDbContext;
      _authService = authService;
    }

    public IActionResult Index()
    {
      return View();
    }
    public IActionResult Error()
    {
      return View();
    }
    [Authorize]
    public IActionResult Secret()
    {
      return View();
    }
    public IActionResult Logout()
    {
      HttpContext.SignOutAsync();
      return RedirectToAction("Index");
    }
    public IActionResult Authenticate()
    {
      var user = HttpContext.User.Identity.Name;

      var grandmaClaims = new List<Claim>()
      {
        new Claim(ClaimTypes.Name, "Bob"),
        new Claim(ClaimTypes.Email, "Bob@exampl.com"),
        new Claim("Grandma.Says", "I like bob"),
      };

      var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");

      var licenseClaims = new List<Claim>()
      {
        new Claim(ClaimTypes.Name, "Bob Alexander"),
        new Claim(ClaimTypes.Email, "Bob.alex@example.com"),
        new Claim("DrivingLicense", "A+"),
      };

      var licenseIdentity = new ClaimsIdentity(licenseClaims, "License Identity");

      var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });

      HttpContext.SignInAsync(userPrincipal).Wait();

      user = HttpContext.User.Identity.Name;

      return RedirectToAction("Index");
    }
    public IActionResult Login()
    {
      return View();
    }
    public async Task<IActionResult> Login(string username, string password)
    {
      var user = await _UserManager.FindByNameAsync(username);
      if(user != null)
      {
        var result = await _SignInManager.PasswordSignInAsync(user, password, false, false);
        if(result.Succeeded)
        {

        }
      }
      return View();
    }
    public IActionResult Register()
    {
      return View();
    }
    public async Task<IActionResult> Register(string username, string password)
    {
      var user = new IdentityUser
      {
        UserName = username,
        Email = "",
      };
      var result = await _UserManager.CreateAsync(user, password);
      if(result.Succeeded)
      {
        //sign user here
      }
      return View();
    }
    [HttpGet]
    public async Task<IActionResult> CustomRegister()
    {
      return View();
    }
    [HttpPost]
    //public async Task<IActionResult> CustomRegister(string username, string password)
    public IActionResult CustomRegister(RegisterViewModel model)
    {
      _authService.RegisterUser(model);
      return RedirectToAction("Index");
    }
  }
}
