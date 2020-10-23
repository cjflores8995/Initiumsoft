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

        public async Task<RequestResponse> UpdateNotificationState(string userId)
        {
            RequestResponse resp = new RequestResponse();

            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {

                    var notifications = db.Notificaciones.Where(
                      n => n.UserId == userId
                      && n.Visto == false
                      ).ToList();

                    if (notifications.Any())
                    {
                        notifications.ForEach(n => n.Visto = true);
                        await (db.SaveChangesAsync());
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    resp = new RequestResponse(ex.Message);
                }
                return resp;
            }
        }

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
