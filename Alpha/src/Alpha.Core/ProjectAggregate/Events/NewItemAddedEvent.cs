using Alpha.SharedKernel;

namespace Alpha.Core.ProjectAggregate.Events
{
  /// <summary>
  /// all relevant event data (and method) for adding item to our todo project
  /// </summary>
  public class NewItemAddedEvent : BaseDomainEvent
  {
    public ToDoItem NewItem { get; set; }
    public Project Project { get; set; }

    public NewItemAddedEvent(Project project,
        ToDoItem newItem)
    {
      Project = project;
      NewItem = newItem;
    }
  }
}
