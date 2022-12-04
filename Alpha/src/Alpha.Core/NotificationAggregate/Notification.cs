using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.SharedKernel;
using Alpha.SharedKernel.Interfaces;

namespace Alpha.Core.NotificationAggregate;
public class Notification : BaseEntity, IAggregateRoot
{
   public Notification()
   {
   }
}
