using BusinessLogic.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace WebApp.Controllers
{
    public class RealtimeController : Controller
    {
        // GET: Realtime
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCountTicketsAsignados()
        {
            RequestResponse resp = new RequestResponse();
            int count = 0;

            try
            {
                count = new NotificacionRepository().CountNotificationsByUserId(User.Identity.GetUserId());
                resp.Details = count.ToString();
            }
            catch(Exception ex)
            {
                resp = new RequestResponse(ex.Message);
                resp.Details = count.ToString();
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}