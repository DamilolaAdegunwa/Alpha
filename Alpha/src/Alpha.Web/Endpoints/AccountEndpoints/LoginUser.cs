using Alpha.Core.ProjectAggregate;
using Alpha.Core.Interfaces;
using Alpha.Core.Models;
using Alpha.SharedKernel.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Alpha.Core.Constants;
using Serilog;

namespace Alpha.Web.Endpoints.AccountEndpoints
{
  public class LoginUser : BaseAsyncEndpoint
    .WithRequest<LoginUserRequest>
  .WithResponse<LoginUserResponse>
  {
    private readonly IAuthService _authService;
    public LoginUser(IAuthService authService)
    {
      _authService = authService;
    }

    [HttpPost("/LoginUser")]
    [SwaggerOperation(
  Summary = "Login user to the platform",
  Description = "...using a simple method",
  OperationId = "Account.LoginUser",
  Tags = new[] { Constants.Endpoints.Account }
  )]
    public override async Task<ActionResult<LoginUserResponse>> HandleAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
      try
      {
        LoginUserResponse response = null;

        //code goes here
        var result = await _authService.LoginUser(new RegisterViewModel { Password = request.Password, Username = request.Username });
        if(!result.status)
        {
          return BadRequest(result.message);
        }

        response = new LoginUserResponse
        {
          Message = result.message,
          User = result.response
        };

        return Ok(response);
      }
      catch (Exception ex)
      {

        Log.Error(ex.Message);
        return BadRequest(ex.Message);
      }
    }
  }
}
