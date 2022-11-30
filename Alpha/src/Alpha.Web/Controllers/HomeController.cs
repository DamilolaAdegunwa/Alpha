using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
  }
}
