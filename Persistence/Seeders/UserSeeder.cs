using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders
{
    public class UserSeeder
    {
        public void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //usuario que asignara los tickets
            var tmpAssignTicketUser = context.Users.FirstOrDefault(x => x.Email.Equals("admin@initiumsoft.com"));

            if (tmpAssignTicketUser == null)
            {
                var assignTicketUser = new ApplicationUser()
                {
                    FirstName = "Carlos",
                    LastName = "Flores",
                    Email = "admin@initiumsoft.com",
                    UserName = "admin@initiumsoft.com",
                    EmailConfirmed = true
                };
                userManager.Create(assignTicketUser, "1234abcd");
                userManager.AddToRole(assignTicketUser.Id, "AssignTicket");
            }

            //usuario que trabajara con los tickets
            var tmpSolveTicketUser = context.Users.FirstOrDefault(x => x.Email.Equals("ticket1@initiumsoft.com"));

            if (tmpSolveTicketUser == null)
            {
                var solveTicketsUser = new ApplicationUser()
                {
                    FirstName = "Jenny",
                    LastName = "Perez",
                    Email = "ticket1@initiumsoft.com",
                    UserName = "ticket1@initiumsoft.com",
                    EmailConfirmed = true
                };
                userManager.Create(solveTicketsUser, "1234abcd");
                userManager.AddToRole(solveTicketsUser.Id, "SolveTicket");
            }

            //segundo usuario que atiende
            //var tmpSolveTicketUser2 = context.Users.FirstOrDefault(x => x.Email.Equals("ticket2@initiumsoft.com"));

            //if (tmpSolveTicketUser == null)
            //{
            //    var solveTicketsUser = new ApplicationUser()
            //    {
            //        FirstName = "Patricia",
            //        LastName = "Galvez",
            //        Email = "ticket2@initiumsoft.com",
            //        UserName = "ticket2@initiumsoft.com",
            //        EmailConfirmed = true
            //    };
            //    userManager.Create(solveTicketsUser, "1234abcd");
            //    userManager.AddToRole(solveTicketsUser.Id, "SolveTicket");
            //}

        }
    }
}
