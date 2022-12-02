using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.SharedKernel;

namespace Alpha.Infrastructure.GroupAggregate.Events;
internal class NewGroupMilestoneReachedEvent : BaseDomainEvent
{
  public NewGroupMilestoneReachedEvent(Group group)
  {
    Group = group;
  }

  public Group Group { get; set; }
}
