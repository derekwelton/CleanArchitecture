using System;
using System.Threading.Tasks;

namespace Application.Common.Identity
{
    public interface IIdentityRepository
    {
        Task<IApplicationUser> CreateAsync(IApplicationUser user);
        Task DeleteAsync(IApplicationUser user);
        Task<IApplicationUser> FindByIdAsync(string userId);
        Task<IApplicationUser> FindByNameAsync(string userName);
    }
}