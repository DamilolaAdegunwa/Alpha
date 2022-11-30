using Alpha.Core.ProjectAggregate;
using Ardalis.Specification;

namespace Alpha.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdWithItemsSpec : Specification<Project>, ISingleResultSpecification
    {
        public ProjectByIdWithItemsSpec(int projectId)
        {
            Query
                .Where(project => project.Id == projectId)
                .Include(project => project.Items);
        }
    }
}