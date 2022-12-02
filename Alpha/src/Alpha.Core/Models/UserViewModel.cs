using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Alpha.Core.Models;
public class UserViewModel
{
  /// <summary>
  /// Gets or sets the primary key for this user.
  /// </summary>
  public  string Id { get; set; }

  /// <summary>
  /// Gets or sets the user name for this user.
  /// </summary>
  public  string UserName { get; set; }

  /// <summary>
  /// Gets or sets the email address for this user.
  /// </summary>
  public  string Email { get; set; }

  /// <summary>
  /// Gets or sets a telephone number for the user.
  /// </summary>
  public  string PhoneNumber { get; set; }
}
