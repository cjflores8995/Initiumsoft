using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class StatusTicket: CommonFields
    {
        public enum EnumEstatusTicket
        {
            ingresado = 1,
            asignado,
            atendido,

        }

        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Siglas { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(80)]
        public string Descripcion { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
