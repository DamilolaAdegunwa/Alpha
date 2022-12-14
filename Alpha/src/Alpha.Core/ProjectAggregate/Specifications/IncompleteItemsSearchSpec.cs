using Ardalis.Specification;

namespace Alpha.Core.ProjectAggregate.Specifications
{
  public class IncompleteItemsSearchSpec : Specification<ToDoItem>
  {
    /// <summary>
    /// constructor implementation of project-related tasks
    /// </summary>
    public IncompleteItemsSearchSpec(string searchString)
    {
      Query
          .Where(item => !item.IsDone &&
          (item.Title.Contains(searchString) ||
          item.Description.Contains(searchString)));
    }
  }
}
