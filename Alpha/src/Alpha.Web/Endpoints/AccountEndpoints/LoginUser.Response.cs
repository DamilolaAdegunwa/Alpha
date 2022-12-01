using Alpha.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Alpha.Web.Endpoints.AccountEndpoints;

public class LoginUserResponse
{
  public UserViewModel User { get; set; }
  public string Message { get; set; }
}
