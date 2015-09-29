using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Security;
using AquaCultureMonitor.Core.Repositories;
using AquaCultureMonitor.Core.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AquaCultureMonitor.Core.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly UserStore<ApplicationUser> _userStore;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(DatabaseContext databaseContext)
        {
            _userStore = new UserStore<ApplicationUser>(databaseContext);
            _userManager = new UserManager<ApplicationUser>(_userStore);
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(databaseContext));
        }

        public List<ApplicationUser> Get()
        {
            return _userManager.Users.ToList();
        }

        public List<string> GetRoles(string id)
        {
            return _userManager.GetRoles(id).ToList();
        }

        public List<string> GetRoles()
        {
            return typeof(UserRoles).GetProperties().Select(role => role.Name).ToList();
        }

        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            IdentityResult idResult = _roleManager.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }

        public List<ApplicationUser> GetUsersByRole(string userRole)
        {
            var userIds = _roleManager.FindByName(userRole).Users.Select(e => e.UserId).ToList();
            var users = _userManager.Users.Where(e => userIds.Contains(e.Id)).ToList();
            return users;
        }

        public void ClearUserRoles(string userId)
        {
            ApplicationUser user = _userManager.FindById(userId);

            // var user = _userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);



            foreach (var currentRole in currentRoles)
            {
                var role = _roleManager.Roles.FirstOrDefault(x => x.Id == currentRole.RoleId);
                _userManager.RemoveFromRole(userId, role.Name);
            }

        }

        public ApplicationUser Find(string username, string password)
        {
            return _userManager.Find(username, password);
        }

        public Task<ApplicationUser> FindAsync(string username, string password)
        {
            return _userManager.FindAsync(username, password);
        }

        public Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            return _userManager.FindAsync(loginInfo);
        }

        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            return _userManager.AddLoginAsync(userId, loginInfo);
        }

        public IList<UserLoginInfo> GetLogins(string userId)
        {
            return _userManager.GetLogins(userId);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public IdentityResult Create(ApplicationUser user, string password)
        {
            return _userManager.Create(user, password);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var password = Membership.GeneratePassword(8, 2);
            return _userManager.CreateAsync(user, password);
        }

        public IdentityResult Create(ApplicationUser user)
        {
            var password = Membership.GeneratePassword(8, 2);
            return _userManager.Create(user, password);
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            return _userManager.UpdateAsync(user);
        }

        public void Update(ApplicationUser user)
        {
            _userManager.Update(user);
            var context = _userStore.Context;
            context.SaveChanges();
        }

        public IdentityResult AddToRole(string userId, string role)
        {
            try { _userManager.AddToRole(userId, role); }
            catch (Exception exception) { Elmah.ErrorSignal.FromCurrentContext().Raise(exception); }
            return new IdentityResult();
        }

        public ApplicationUser FindById(string userId)
        {
            return _userManager.FindById(userId);
        }

        public ApplicationUser FindByName(string userName)
        {
            return _userManager.FindByName(userName);
        }

        public ApplicationUser FindByEmail(string emailAddress)
        {
            return _userManager.FindByEmail(emailAddress);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string emailAddress)
        {
            return await _userManager.FindByEmailAsync(emailAddress);
        }

        public Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo userLoginInfo)
        {
            return _userManager.RemoveLoginAsync(userId, userLoginInfo);
        }

        public Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
        }

        public Task<IdentityResult> AddPasswordAsync(string userId, string password)
        {
            return _userManager.AddPasswordAsync(userId, password);
        }

        public IdentityResult RemovePassword(string userId)
        {
            return _userManager.RemovePassword(userId);
        }

        public IdentityResult AddPassword(string userId, string password)
        {
            return _userManager.AddPassword(userId, password);
        }

        public IdentityResult SetPassword(string userId)
        {
            var password = Membership.GeneratePassword(8, 2);
            try { _userManager.AddPassword(userId, password); }
            catch (Exception exception) { Elmah.ErrorSignal.FromCurrentContext().Raise(exception); }
            return new IdentityResult();
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            return _userManager.CreateIdentityAsync(user, authenticationType);
        }

        public ClaimsIdentity CreateIdentity(ApplicationUser user, string authenticationType)
        {
            return _userManager.CreateIdentity(user, authenticationType);
        }
    }
}