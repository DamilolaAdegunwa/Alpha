using Alpha.Core.ProjectAggregate.Events;
using Alpha.SharedKernel;

namespace Alpha.Core.ProjectAggregate
{
  public class ToDoItem : BaseEntity
  {
    public ToDoItem()
    {
      
    }
    #region TodoItem properties
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; private set; }
    public TodoItemPriority TodoItemPriority { get; set; }
    #endregion

    public void MarkComplete()
    {
      if (!IsDone)
      {
        IsDone = true;
        //this just adds the present time to the event list
        Events.Add(new ToDoItemCompletedEvent(this));
      }
    }

    public override string ToString()
    {
      string status = IsDone ? "Done!" : "Not done.";
      return $"{Id}: Status: {status} - {Title} - {Description}";
    }
  }
}
