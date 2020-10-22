using BusinessLogic.Repository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace WebApp.Controllers
{
    public class LogicController : Controller
    {
        // GET: Logic
        public ActionResult AddNewTicket(MainModel model)
        {
            RequestResponse resp = new RequestResponse();

            try
            {
                Ticket ticket = new Ticket()
                {
                    CodigoTicket = model.Ticket.CodigoTicket,
                    Nombre = model.Ticket.Nombre,
                    Queue = int.Parse(model.Ticket.Detalles),
                    Detalles = string.Empty,
                    UserId = model.Ticket.ApplicationUser.Id,
                    IdEstatusTicket = (int)StatusTicket.EnumEstatusTicket.asignado
                };

                resp = new TicketRepository().AgregarTicket(ticket);

                if(resp.Status)
                {
                    resp.Details = Url.Action("Ingresar", "Ticket");
                }

            } 
            catch(Exception ex)
            {
                resp = new RequestResponse(ex.Message);
            }

            return Json(resp);
        }
    }
}