using BusinessLogic.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class UserRepository: Repository
    {
        public string GetRoleUser(string userId)
        {
            string role = string.Empty;

            try
            {
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                role = userManager.GetRoles(userId).FirstOrDefault();
            } 
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return role;
        }
    
        public IEnumerable<ApplicationUser> GetAllUsers(string role = "")
        {
            IEnumerable<ApplicationUser> usuarios;
            string roleId = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(role))
                {
                    usuarios = from u in db.Users
                               select u;
                }
                else
                {
                    if (role.Equals(CustomEnums.TicketRoles.AssignTicket.ToString()))
                    {
                        roleId = db.Roles.
                            FirstOrDefault(r => r.Name.Equals(CustomEnums.TicketRoles.AssignTicket.ToString())).Id;
                    }
                    else
                    {
                        roleId = db.Roles.
                            FirstOrDefault(r => r.Name.Equals(CustomEnums.TicketRoles.SolveTicket.ToString())).Id;
                    }

                    usuarios = from u in db.Users
                               where u.Roles.Any(r => r.RoleId.Equals(roleId))
                               select u;
                }
            } catch(Exception ex)
            {
                usuarios = null;
                System.Diagnostics.Debug.WriteLine(ex.Message); 
            }

            return usuarios;
        }
    }
}
