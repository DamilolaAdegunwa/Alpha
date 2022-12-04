using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Core.PostAggregate.Events;
using MediatR;
using Serilog;

namespace Alpha.Core.PostAggregate.Handlers;
public class RenameAttachmentNotificationHandlers : INotificationHandler<NewAttachmentAddedEvent>
{
   public async Task Handle(NewAttachmentAddedEvent notification, CancellationToken cancellationToken)
   {
      Log.Information($"attachment renamed has been reviewed!");
      //throw new NotImplementedException();
   }
}
