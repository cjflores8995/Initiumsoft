using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public abstract class CommonFields
    {
        public CommonFields()
        {
            this.FechaRegistro = DateTime.UtcNow;
            this.FechaModificacion = DateTime.UtcNow;
            this.Estado = true;
        }

        public DateTime FechaRegistro { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
