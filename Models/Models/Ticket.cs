using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Ticket: CommonFields
    {
        public int Id { get; set; }

        public string CodigoTicket { get; set; }
        public string Nombre { get; set; }
        public int Queue { get; set; }
        public string Detalles { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("StatusTicket")]
        public int IdEstatusTicket { get; set; }
        public virtual StatusTicket StatusTicket { get; set; }
    }
}
