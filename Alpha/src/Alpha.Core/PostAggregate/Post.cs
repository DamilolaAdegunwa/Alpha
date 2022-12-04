using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Alpha.Core.ProjectAggregate.Events;
using Alpha.Core.ProjectAggregate;
using Alpha.SharedKernel;
using Alpha.SharedKernel.Interfaces;
using Ardalis.GuardClauses;
using Alpha.Core.PostAggregate.Events;
using Alpha.Core.Models.Entities;
using Alpha.Core.CommentAggregate;

namespace Alpha.Core.PostAggregate;
public class Post : BaseEntity, IAggregateRoot
{
   [Required]
   public string Title { get; set; }
   [Required]
   public string Message { get; set; }
   [Required]
   public string AuthorId { get; set; }
   private List<TodoAttachment> _attachments => new List<TodoAttachment>();
   public IEnumerable<TodoAttachment> Attachments => _attachments.AsReadOnly();
   public bool IsHtml { get; set; } = true;
   private List<Comment> _comments => new List<Comment>();
   public IEnumerable<Comment> Comments => _comments.AsReadOnly();

   public void AddAttachemt(TodoAttachment attachment)
   {
      Guard.Against.Null(attachment, nameof(attachment));
      _attachments.Add(attachment);

      var newItemAddedEvent = new NewAttachmentAddedEvent(this, attachment);
      Events.Add(newItemAddedEvent);
   }
}
