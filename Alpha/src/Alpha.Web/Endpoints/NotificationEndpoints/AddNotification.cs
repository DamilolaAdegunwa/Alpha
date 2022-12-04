using Alpha.Core.Constants;
using Alpha.Web.Endpoints.NotificationEndpoints.Payload;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Alpha.Web.Endpoints.NotificationEndpoints;

public class AddNotification : BaseAsyncEndpoint.WithRequest<NotificationRequest>.WithResponse<NotificationResponse>
{

   [HttpPost("/Notification")]
   [SwaggerOperation(
            Summary = "Creates a new Notification",
            Description = "Creates a new Notification",
            OperationId = "Notification.Create",
            Tags = new[] { Constants.Endpoints.Notification })
        ]
   public override async Task<ActionResult<NotificationResponse>> HandleAsync(NotificationRequest request, CancellationToken cancellationToken = default)
   {
      return Ok(new NotificationResponse { Message = "Successful!" });
      //throw new NotImplementedException();
   }
}
