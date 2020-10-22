using BusinessLogic.Repository;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Utilities;
using WebApp.Hubs;

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
                    //asigna ruta de recireccion
                    resp.Details = Url.Action("Ingresar", "Ticket");

                    //crea el obtejo de notificacion
                    var notificacion = new Notificacion()
                    {
                        UserId = model.Ticket.ApplicationUser.Id,
                        Titulo = $"Ticket {model.Ticket.CodigoTicket} asignado",
                        Mensaje = $"El administrador te asigno el ticket {model.Ticket.CodigoTicket} para tu atención.",
                        Url = Url.Action("Asignados", "Ticket", new { Id = model.Ticket.CodigoTicket })
                    };

                    //guarda notificacion
                    var notyResp = new NotificacionRepository().AgregarNotificacionAsync(notificacion);

                    //dispara la notificacion
                    RealtimeHub.Show();
                }

            } 
            catch(Exception ex)
            {
                resp = new RequestResponse(ex.Message);
            }

            return Json(resp);
        }

        public ActionResult UpdateTicket(MainModel model)
        {
            RequestResponse resp = new RequestResponse();

            try
            {
                Ticket currentTicket = new TicketRepository().ObtenerTicketPorId(model.Ticket.Id);

                if (currentTicket == null || !currentTicket.StatusTicket.Siglas.Equals(StatusTicket.EnumEstatusTicket.asignado.ToString()))
                    throw new ApplicationException("El ticket esta vacio o ya se encuentra atendido.");

                currentTicket.Detalles = JsonConvert.SerializeObject(new { Init = model.GenericString, End = DateTime.Now.ToString("HH:mm:ss") });
                currentTicket.IdEstatusTicket = (int)StatusTicket.EnumEstatusTicket.atendido;
                currentTicket.FechaModificacion = DateTime.UtcNow.AddHours(-5);

                resp = new TicketRepository().ActualizarTicket(currentTicket);

                if (resp.Status)
                {
                    resp.Details = Url.Action("Asignados", "Ticket");
                }

            }
            catch (Exception ex)
            {
                resp = new RequestResponse(ex.Message);
            }

            return Json(resp);
        }
    }
}