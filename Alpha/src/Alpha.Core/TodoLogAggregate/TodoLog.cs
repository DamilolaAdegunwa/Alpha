using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.SharedKernel;
using Alpha.SharedKernel.Interfaces;

namespace Alpha.Core.TodoLogAggregate;
public class TodoLog : BaseEntity, IAggregateRoot
{
   public TodoLogAction Action { get; set; }
   public string Title { get; set; }
   public string Message { get; set; }
}
