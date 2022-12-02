using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.SharedKernel.Interfaces;
using Alpha.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Alpha.Infrastructure.UserAggregate;
using Alpha.Core.ProjectAggregate.Events;
using Alpha.Core.ProjectAggregate;
using Ardalis.GuardClauses;
using SQLitePCL;
using Alpha.Infrastructure.GroupAggregate.Events;

namespace Alpha.Infrastructure.GroupAggregate;

public class Group : BaseEntity, IAggregateRoot
{
  public Group()
  {

  }

  //properties
  public int Id { get; set; }
  private List<ApplicationUser> _applicationUsers = new List<ApplicationUser>();
  public IEnumerable<ApplicationUser> ApplicationUsers => _applicationUsers.AsReadOnly();
  public string Name { get; set; }
  public string Description { get; set; }
  public GroupType GroupType { get; set; }
  public GroupStatus GroupStatus { get; set; }

  public void AddUser(ApplicationUser user)
  {
    Guard.Against.Null(user, nameof(user));
    _applicationUsers.Add(user);

    var newUserAddedEvent = new NewUserAddedEvent(this, user);
    Events.Add(newUserAddedEvent);
  }

  public void UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
  }
  //-------
  //(properties) id, list of users, name, descrption, type 
  //(events) created,completed, milestone reached
  //(handlers - group-related functions) alert users, remainders, deadlines
  //(specification - db related calls)
}
