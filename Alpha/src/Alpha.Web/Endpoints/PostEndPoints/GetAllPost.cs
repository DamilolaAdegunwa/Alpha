using Alpha.Web.Endpoints.PostEndPoints.Payload;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Alpha.Core.Constants;
namespace Alpha.Web.Endpoints.PostEndPoints;

public class GetAllPost : BaseAsyncEndpoint.WithRequest<PostRequest>.WithResponse<PostResponse>
{
   [HttpGet("/GetAllPost")]
   [SwaggerOperation(
         Summary = "",
         Description = "",
         OperationId = "Post.GetAllPost",
         Tags = new[] { Constants.Endpoints.Post }
         )]
   public override async Task<ActionResult<PostResponse>> HandleAsync(PostRequest request, CancellationToken cancellationToken = default)
   {
      return Ok($"get all post!");
      //throw new NotImplementedException();
   }
}
