using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApp.Hubs
{
    public class RealtimeHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<RealtimeHub>();
            context.Clients.All.displayNotifications();
        }
    }
}