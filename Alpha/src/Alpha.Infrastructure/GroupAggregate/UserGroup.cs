using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.SharedKernel.Interfaces;
using Alpha.SharedKernel;

namespace Alpha.Infrastructure.GroupAggregate;
public class UserGroup : BaseEntity, IAggregateRoot
{
  public UserGroup()
  {
  }
}
