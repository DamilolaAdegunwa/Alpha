using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Core.ProjectAggregate;
using Ardalis.Specification;

namespace Alpha.Core.PostAggregate.Specifications;
public class PostByIdWithAttachmentsAndCommentsSpec : Specification<Post>, ISingleResultSpecification
{
   public PostByIdWithAttachmentsAndCommentsSpec(int postId)
   {
      Query
         .Where(p => p.Id == postId)
         .Include(p => p.Comments)
         .Include(p => p.Attachments);
   }
}
