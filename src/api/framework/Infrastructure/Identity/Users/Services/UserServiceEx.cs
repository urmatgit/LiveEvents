using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.Framework.Core.Identity.Users.Abstractions;
using FSH.Framework.Core.Identity.Users.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FSH.Framework.Infrastructure.Identity.Users.Services;
internal sealed partial class UserService : IUserService
{
    public async Task<List<UserDetail>> GetListByRoleAsync(string role, CancellationToken cancellationToken)
    {
        var users = await userManager.GetUsersInRoleAsync(role);
        return users.Adapt<List<UserDetail>>();

    }

    public async Task<List<UserDetail>> GetListByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        var users =await  userManager.Users
            .Where(u => ids.Any(i => i.ToString() == u.Id))
            .ToListAsync(cancellationToken);
        return users.Adapt<List<UserDetail>>();
    }
}
