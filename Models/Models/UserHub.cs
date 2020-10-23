using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserHub
    {
        public string UserId { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}
