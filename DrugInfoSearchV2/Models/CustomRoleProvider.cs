using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DrugInfoSearchV2.Models
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string userId)
        {
            using (var db = new DrugInfoContext())
            {
                int id = int.Parse(userId);
                var user = db.Users
                    .Where(u => u.UserId == id)
                    .FirstOrDefault();

                if (user != null)
                {
                    string[] roles = user.UserRole.Select(item => item.Roles.RoleName).ToArray();
                    return roles;
                }
                return new string[] { };
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string userId, string roleName)
        {
            using (var db = new DrugInfoContext())
            {
                int id = int.Parse(userId);
                var user = db.Users
                    .Where(u => u.UserId == id)
                    .FirstOrDefault();

                if (user != null)
                {
                    string[] roles = user.UserRole.Select(item => item.Roles.RoleName).ToArray();

                    if (roles.Contains(roleName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}