using Alpha.Core.Models.DTOs;

namespace Alpha.Web.Endpoints.AccountEndpoints
{
   public class RegisterUserResponse
  {
    public string Message { get; set; }
    public UserViewModel User { get; set; }
  }
}
