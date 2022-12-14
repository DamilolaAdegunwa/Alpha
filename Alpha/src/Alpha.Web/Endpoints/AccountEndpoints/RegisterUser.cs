using Alpha.Core.ProjectAggregate;
using Alpha.Core.Interfaces;
using Alpha.Core.Models;
using Alpha.SharedKernel.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Alpha.Core.Constants;
using Serilog;
using Alpha.Core.Models.DTOs;
namespace Alpha.Web.Endpoints.AccountEndpoints
{
  public class RegisterUser : BaseAsyncEndpoint
    .WithRequest<RegisterUserRequest>
    .WithResponse<RegisterUserResponse>
  {
    private readonly IAuthService _authService;
    public RegisterUser(IAuthService authService)
    {
      _authService = authService;
    }
    [HttpPost("/RegisterUser")]
    [SwaggerOperation(
      Summary = "Register a new user to the platform",
      Description = "...using a simple method that just takes in the basic details to justt have a working user table",
      OperationId = "Account.RegisterUser",
      Tags = new[] { Constants.Endpoints.Account }
      )]
    public override async Task<ActionResult<RegisterUserResponse>> HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
      try
      {
        if (request == null) { return BadRequest("empty request"); }

        var result = await _authService.RegisterUser(new RegisterViewModel { Password = request.Password, Username = request.Username });

        if (!result.status)
        {
          return BadRequest(result.message);
        }
        return Ok(new RegisterUserResponse { Message = result.message, User = result.response });
      }
      catch (Exception ex)
      {
        var errorMessage = $"Error message: {ex.Message}, Error StackTrace: {ex.StackTrace}, Error InnerException: {ex.InnerException?.Message} {ex.InnerException?.StackTrace}";
        Log.Error(errorMessage);
        return BadRequest(ex.Message);
      }
    }
  }
}
