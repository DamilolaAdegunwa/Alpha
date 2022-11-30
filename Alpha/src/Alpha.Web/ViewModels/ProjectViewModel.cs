using System.Collections.Generic;

namespace Alpha.Web.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ToDoItemViewModel> Items = new();
    }
}
