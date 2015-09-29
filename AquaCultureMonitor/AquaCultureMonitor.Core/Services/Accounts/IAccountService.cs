using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AquaCultureMonitor.Core.Services.Accounts
{
    public interface IAccountService
    {
        List<ApplicationUser> Get();
        List<string> GetRoles(string userId);
        List<string> GetRoles();
        List<ApplicationUser> GetUsersByRole(string userRole);
        bool RoleExists(string name);
        bool CreateRole(string name);
        void ClearUserRoles(string userId);

        // Identity 
        ApplicationUser Find(string username, string password);
        Task<ApplicationUser> FindAsync(string username, string password);
        Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo);
        IList<UserLoginInfo> GetLogins(string userId);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        IdentityResult Create(ApplicationUser user, string password);
        IdentityResult Create(ApplicationUser user);

        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        void Update(ApplicationUser user);
        IdentityResult AddToRole(string userId, string role);
        ApplicationUser FindById(string userId);
        ApplicationUser FindByName(string emailAddress);
        ApplicationUser FindByEmail(string emailAddress);
        Task<ApplicationUser> FindByEmailAsync(string emailAddress);
        Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo userLoginInfo);
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<IdentityResult> AddPasswordAsync(string userId, string password);
        IdentityResult RemovePassword(string userId);
        IdentityResult AddPassword(string userId, string password);
        IdentityResult SetPassword(string userId);
        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType);
        ClaimsIdentity CreateIdentity(ApplicationUser user, string authenticationType);
    }
}
