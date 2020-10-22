using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Notificacion: CommonFields
    {
        public Notificacion()
        {
            this.Visto = false;
        }

        public int Id { get; set; }

        [Required]
        public bool Visto { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Mensaje { get; set; }

        public string Url { get; set; }
        public int Parametros { get; set; }
    }
}
