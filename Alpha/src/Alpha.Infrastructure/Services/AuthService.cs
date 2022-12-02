using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Alpha.Core.ProjectAggregate;
using Alpha.Infrastructure.Data;
using Alpha.Core.Helper;
using Alpha.Core.Interfaces;
using Alpha.Core.Models;
using Alpha.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Alpha.Infrastructure.Services;
public class AuthService : IAuthService
{
  private readonly UserManager<IdentityUser> _UserManager;
  private readonly SignInManager<IdentityUser> _SignInManager;
  private readonly AppDbContext _appDbContext;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
  {
    _UserManager = userManager;
    _SignInManager = signInManager;
    _appDbContext = appDbContext;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<(bool status, string message, UserViewModel response)> LoginUser(RegisterViewModel model)
  {
    // try catch (logs, retry, page-name and method-name, initialize variables)
    Log.Information($"---now in the '{nameof(LoginUser)}' method---");
    var methodName = nameof(LoginUser);
    try
    {
      if (model == null) { throw new ArgumentNullException("model"); }

      if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Username)) { return (false, "pls include valid data!",null); }

      var appUser = _appDbContext.ApplicationUser.FirstOrDefault(u => u.UserName == model.Username && u.PasswordHash == Stringhelper.SHA512(model.Password));

      if (appUser == null) { return (false, "does not exist", null); }

      var alphaClaims = new List<Claim>{
      new Claim(ClaimTypes.Name, model.Username),
    };

      var alphaIdentity = new ClaimsIdentity(alphaClaims, model.Username);

      var alphaPrincipal = new ClaimsPrincipal(alphaIdentity);

      _httpContextAccessor.HttpContext.SignInAsync(alphaPrincipal).Wait();

      var user = _httpContextAccessor.HttpContext.User.Identity.Name;

      var uservm = new UserViewModel
      {
        Email = appUser.Email,
        Id = appUser.Id,
        PhoneNumber = appUser.PhoneNumber,
        UserName = appUser.UserName
      };

      return (true, "success", uservm);
    }
    catch (Exception ex)
    {
      var errorMessage = $"Error message: {ex.Message}, Error StackTrace: {ex.StackTrace}, Error InnerException: {ex.InnerException?.Message} {ex.InnerException?.StackTrace}";
      return (false, errorMessage, null);
    }
  }

  public async Task<(bool status, string message)> LogoutUser(RegisterViewModel model)
  {
    // try catch (logs, retry, page-name and method-name, initialize variables)
    Log.Information($"---now in the '{nameof(LogoutUser)}' method---");
    var methodName = nameof(LogoutUser);
    try
    {
      await _httpContextAccessor.HttpContext.SignOutAsync();
      return (true, "successful!");
    }
    catch (Exception ex)
    {
      var errorMessage = $"Error message: {ex.Message}, Error StackTrace: {ex.StackTrace}, Error InnerException: {ex.InnerException?.Message} {ex.InnerException?.StackTrace}";
      Log.Error(errorMessage);
      return (false, errorMessage);
    }
  }

  public async Task<(bool status, string message, UserViewModel response)> RegisterUser(RegisterViewModel model)
  {
    // try catch (logs, retry, page-name and method-name, initialize variables)
    Log.Information($"---now in the '{nameof(RegisterUser)}' method---");
    var methodName = nameof(RegisterUser);
    try
    {
      if (model == null) return (false, "null value", null);

      var chkUser = _appDbContext.ApplicationUser.FirstOrDefault(u => u.UserName.Equals(model.Username,StringComparison.OrdinalIgnoreCase));

      if (chkUser != null) { return (false, "user already exist!", null); }

      string hash = Stringhelper.SHA512(model.Password);
      var user = new IdentityUser
      {
        UserName = model.Username,
        PasswordHash = hash,
      };
      _appDbContext.Add<IdentityUser>(user);
      _appDbContext.SaveChanges();

      var appUser = _appDbContext.ApplicationUser.FirstOrDefault(u => u.UserName == model.Username && u.PasswordHash == Stringhelper.SHA512(model.Password));

      if (appUser == null) { return (false, "does not exist", null); }

      var uservm = new UserViewModel
      {
        Email = appUser.Email,
        Id = appUser.Id,
        PhoneNumber = appUser.PhoneNumber,
        UserName = appUser.UserName,
      };
      return (true, "successful", uservm);
    }
    catch (Exception ex)
    {
      var errorMessage = $"Error message: {ex.Message}, Error StackTrace: {ex.StackTrace}, Error InnerException: {ex.InnerException?.Message} {ex.InnerException?.StackTrace}";
      Log.Error(errorMessage);
      return (false, ex.Message, null);
    }
  }
}
