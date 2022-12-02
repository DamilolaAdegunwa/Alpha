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
  public class LogoutUser : BaseAsyncEndpoint
  .WithRequest<LogoutUserRequest>
  .WithResponse<LogoutUserResponse>

  {
    private readonly IAuthService _authService;
    public LogoutUser(IAuthService authService)
    {
      _authService = authService;
    }
    [HttpPost("/LogoutUser")]
    [SwaggerOperation(
      Summary = "Logout user in the platform",
      Description = "...simple signout",
      OperationId = "Account.LogoutUser",
      Tags = new[] { Constants.Endpoints.Account }
      )]
    public override async Task<ActionResult<LogoutUserResponse>> HandleAsync(LogoutUserRequest request, CancellationToken cancellationToken = default)
    {
      try
      {
        LogoutUserResponse response = new LogoutUserResponse { };

        //code goes here
        var result = await _authService.LogoutUser(new RegisterViewModel { });
        if(!result.status)
        {
          return BadRequest(result.message);
        }
        response.Message = result.message;
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
