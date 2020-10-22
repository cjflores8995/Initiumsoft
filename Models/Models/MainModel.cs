using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class MainModel
    {
        public MainModel()
        {
            this.PageTitle = string.Empty;
        }

        public string GenericString { get; set; }
        public int GenericInteger { get; set; }
        public DateTime GenericDateTime { get; set; }

        public string PageTitle { get; set; }
        public string Role { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public Ticket Ticket { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
