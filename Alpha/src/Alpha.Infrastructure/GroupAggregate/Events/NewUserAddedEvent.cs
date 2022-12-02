using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.SharedKernel;
using Alpha.Infrastructure.UserAggregate;

namespace Alpha.Infrastructure.GroupAggregate.Events;
public class NewUserAddedEvent : BaseDomainEvent
{
  public Group Group { get; set; }
  public ApplicationUser User { get; set; }
  public NewUserAddedEvent(Group group, ApplicationUser user)
  {
    Group = group;
    User = user;
  }
}
