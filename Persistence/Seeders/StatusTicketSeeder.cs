using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders
{
    public class StatusTicketSeeder
    {
        public void Seed(ApplicationDbContext context)
        {
            context.StatusTickets.AddOrUpdate(x => x.Siglas,
                new StatusTicket { Siglas = "ingresado", Nombre = "Ingresado", Descripcion = "El ticket ha sido ingresado." },
                new StatusTicket { Siglas = "asignado", Nombre = "Asignado", Descripcion = "El ticket ha sido adignado." },
                new StatusTicket { Siglas = "atendido", Nombre = "Atendido", Descripcion = "El ticket ha sido atendido"}
                );
        }
    }
}
