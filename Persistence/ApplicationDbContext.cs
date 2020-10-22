using Microsoft.AspNet.Identity.EntityFramework;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base("name=MainConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<StatusTicket> StatusTickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<StatusTicket>().ToTable("StatusTickets");

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
