using Alpha.Core.Constants;
using Alpha.Web.Endpoints.PostEndPoints.Payload;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Alpha.Web.Endpoints.PostEndPoints;

public class AddPost : BaseAsyncEndpoint.WithRequest<PostRequest>.WithResponse<PostResponse>
{

   [HttpPost("/Post")]
   [SwaggerOperation(
            Summary = "Creates a new Post",
            Description = "Creates a new Post",
            OperationId = "Post.Create",
            Tags = new[] { Constants.Endpoints.Post })
        ]
   public override async Task<ActionResult<PostResponse>> HandleAsync(PostRequest request, CancellationToken cancellationToken = default)
   {
      //to be implemmented 
      return Ok("post request gotten and saved!");
   }
}
