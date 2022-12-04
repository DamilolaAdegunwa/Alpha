using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Core.Models.Entities;
using Alpha.SharedKernel;

namespace Alpha.Core.PostAggregate.Events;
public class NewAttachmentAddedEvent : BaseDomainEvent
{
   public Post Post { get; set; }
   public TodoAttachment Attachment { get; set; }
   public NewAttachmentAddedEvent(Post post, TodoAttachment attachment)
   {
      Post = post;
      Attachment = attachment;
   }
}
