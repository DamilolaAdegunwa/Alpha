using Alpha.Web.Endpoints.DashboardEndpoints.Payload;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Alpha.Core.Constants;
namespace Alpha.Web.Endpoints.DashboardEndpoints;

public class GetProjectSummary : BaseAsyncEndpoint.WithRequest<ProjectSummaryRequest>.WithResponse<ProjectSummaryResponse>
{


   [HttpGet("/ProjectSummary")]
   [SwaggerOperation(
            Summary = "get project summary",
            Description = "get project summary",
            OperationId = "ProjectSummary.Get",
            Tags = new[] { Constants.Endpoints.Dashboard })
        ]
   public override async Task<ActionResult<ProjectSummaryResponse>> HandleAsync(ProjectSummaryRequest request, CancellationToken cancellationToken = default)
   {
      return Ok(new ProjectSummaryResponse { Message = "successful!" });
      //throw new NotImplementedException();
   }
}
