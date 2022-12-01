using Ardalis.Specification;

namespace Alpha.Core.ProjectAggregate.Specifications
{
    public class IncompleteItemsSpec : Specification<ToDoItem>
    {
        /// <summary>
        /// constructor implementation of project-related tasks
        /// </summary>
        public IncompleteItemsSpec()
        {
            Query.Where(item => !item.IsDone);
        }
    }
}
