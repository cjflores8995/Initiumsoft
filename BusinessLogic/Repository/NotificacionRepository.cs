using BusinessLogic.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BusinessLogic.Repository
{
    public class NotificacionRepository : Repository<Notificacion>
    {
        //agregar ticket
        public async Task<RequestResponse> AgregarNotificacionAsync(Notificacion notificacion)
        {
            RequestResponse resp = new RequestResponse();

            try
            {
                db.Entry(notificacion).State = EntityState.Added;
                await (db.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                resp = new RequestResponse(ex.Message);
            }
            return resp;
        }

        public int CountNotificationsByUserId(string userId)
        {
            return db.Notificaciones.Where(
                n => !n.Visto
                && n.UserId.Equals(userId)
                ).Count();
        }
    }
}
