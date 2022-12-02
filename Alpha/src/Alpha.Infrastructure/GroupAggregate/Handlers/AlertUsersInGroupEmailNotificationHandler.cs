using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Core.ProjectAggregate.Events;
using Alpha.Infrastructure.GroupAggregate.Events;
using MediatR;

namespace Alpha.Infrastructure.GroupAggregate.Handlers;
public class AlertUsersInGroupEmailNotificationHandler : INotificationHandler<AlertUserEvent>
{
   public Task Handle(AlertUserEvent notification, CancellationToken cancellationToken)
   {
      throw new NotImplementedException();
   }
}
