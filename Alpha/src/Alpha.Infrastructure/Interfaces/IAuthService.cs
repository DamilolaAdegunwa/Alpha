using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Infrastructure.Models;

namespace Alpha.Infrastructure.Interfaces;
public interface IAuthService
{
  Task<(bool status, string message)> RegisterUser(RegisterViewModel model);
  Task<(bool status, string message, UserViewModel response)> LoginUser(RegisterViewModel model);
  Task<(bool status, string message)> LogoutUser(RegisterViewModel model);
}
