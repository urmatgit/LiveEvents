using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.Framework.Core.Identity.Users.Dtos;

namespace FSH.Framework.Core.Identity.Users.Abstractions;
public partial interface IUserService
{
    Task<List<UserDetail>> GetListByRoleAsync(string role, CancellationToken cancellationToken);
    Task<List<UserDetail>> GetListByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
}
