using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Models.Models;
using Persistence;

namespace WebApp.Hubs
{
    public class RealtimeHub : Hub
    {
        private static readonly ConcurrentDictionary<string, UserHub> Users = new ConcurrentDictionary<string, UserHub>(StringComparer.InvariantCultureIgnoreCase);
        private ApplicationDbContext db = new ApplicationDbContext();

        public void GetNotification()
        {
            try
            {
                string loggedUser = Context.User.Identity.GetUserId();
                string connectionId = Context.ConnectionId;

                string totalNotif = LoadNotifData(loggedUser);

                var context = GlobalHost.ConnectionManager.GetHubContext<RealtimeHub>();
                context.Clients.Client(connectionId).broadcastNotif(totalNotif);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void SendNotification(string SendTo)
        {
            try
            {

                string totalNotif = LoadNotifData(SendTo);

                var context = GlobalHost.ConnectionManager.GetHubContext<RealtimeHub>();
                context.Clients.All.broadcastNotif2(totalNotif);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private string LoadNotifData(string userId)
        {
            int total = 0;
            var query = (from t in db.Notificaciones
                         where t.UserId == userId
                         && !t.Visto
                         select t).ToList();

            total = query.Count;

            return total.ToString();
        }

        public override Task OnConnected()
        {
            string userId = Context.User.Identity.GetUserId();
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userId, _ => new UserHub
            {
                UserId = userId,
                ConnectionIds = new HashSet<string>()
            }); ;

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userId);
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.GetUserId();
            string connectionId = Context.ConnectionId;

            UserHub user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

                    if (!user.ConnectionIds.Any())
                    {
                        UserHub removedUser;
                        Users.TryRemove(userName, out removedUser);
                        Clients.Others.userDisconnected(userName);
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}