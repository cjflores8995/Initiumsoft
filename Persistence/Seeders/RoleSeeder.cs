using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders
{
    public class RoleSeeder
    {
        public void Seed(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name,
                new IdentityRole("AssignTicket"),
                new IdentityRole("SolveTicket"));
        }
    }
}
