using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Alpha.Core.Constants;
namespace Alpha.Web.Endpoints.AdminEndpoints;

public class DeleteProjectById : BaseAsyncEndpoint.WithRequest<int>.WithResponse<string>
{

   [HttpDelete("/Project")]
   [SwaggerOperation(
            Summary = "Delete project",
            Description = "Delete project",
            OperationId = "Project.Delete",
            Tags = new[] { Constants.Endpoints.Admin })
        ]
   public override async Task<ActionResult<string>> HandleAsync(int request, CancellationToken cancellationToken = default)
   {
      return Ok("successfully deleted!");
      //throw new NotImplementedException();
   }
}
