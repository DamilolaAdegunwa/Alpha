using Alpha.Core.ProjectAggregate;
using Ardalis.Specification;

namespace Alpha.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdWithItemsSpec : Specification<Project>, ISingleResultSpecification
    {
      /// <summary>
      /// constructor implementation of project-related tasks
      /// </summary>
      public ProjectByIdWithItemsSpec(int projectId)
      {
          Query
              .Where(project => project.Id == projectId)
              .Include(project => project.Items);
      }
    }
}
