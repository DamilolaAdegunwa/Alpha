using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Core.CommentAggregate;
using Alpha.SharedKernel;

namespace Alpha.Core.PostAggregate.Events;
public class NewCommentAddedEvent : BaseDomainEvent
{
   public Post Post { get; set; }
   public Comment Comment { get; set; }
   public NewCommentAddedEvent(Post post, Comment comment)
   {
      Post = post;
      Comment = comment;
   }
}
