using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Core.PostAggregate.Events;
using Alpha.Core.ProjectAggregate.Events;
using MediatR;
using Serilog;
namespace Alpha.Core.PostAggregate.Handlers;
public class ReviewAttachmentNotificationHandlers : INotificationHandler<NewAttachmentAddedEvent>
{
   public async Task Handle(NewAttachmentAddedEvent notification, CancellationToken cancellationToken)
   {
      Log.Information($"attachment has been reviewed!");
      //throw new NotImplementedException();
   }
}
