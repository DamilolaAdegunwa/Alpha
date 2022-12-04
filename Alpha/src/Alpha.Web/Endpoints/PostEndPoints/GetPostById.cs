using Alpha.Web.Endpoints.PostEndPoints.Payload;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Alpha.Core.Constants;
namespace Alpha.Web.Endpoints.PostEndPoints;

public class GetPostById : BaseAsyncEndpoint.WithRequest<PostRequest>.WithResponse<PostResponse>
{
   [HttpGet("/GetPostById")]
   [SwaggerOperation(
         Summary = "",
         Description = "",
         OperationId = "Post.GetPostById",
         Tags = new[] { Constants.Endpoints.Post }
         )]
   public override async Task<ActionResult<PostResponse>> HandleAsync(PostRequest request, CancellationToken cancellationToken = default)
   {
      return Ok("get post by id!");
   }
}
