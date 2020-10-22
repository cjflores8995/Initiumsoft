using BusinessLogic.Repository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class RouteController : Controller
    {

        public ActionResult Index()
        {
            return HttpNotFound();
        }

        public ActionResult HandleAssignedRoute(MainModel mainModel, string routeParam = null)
        {
            ActionResult Result;

            try
            {
                if(routeParam == null || string.IsNullOrEmpty(routeParam))
                {
                    mainModel.PageTitle = "Tickets asignados";
                    mainModel.Tickets = new TicketRepository().ObtenerTickets(mainModel, StatusTicket.EnumEstatusTicket.asignado.ToString()).ToList();

                    Result = View("~/Views/Ticket/Asignados.cshtml", mainModel);
                } 
                else 
                {
                    if (mainModel.Role.Equals(BusinessLogic.Common.CustomEnums.TicketRoles.AssignTicket.ToString()))
                    {
                        return HttpNotFound();
                    }

                    mainModel.Ticket = new TicketRepository().ObtenerTicketPorCodigo(mainModel.applicationUser.Id, routeParam);

                    if(mainModel.Ticket == null)
                    {
                        return HttpNotFound();
                    }

                    mainModel.PageTitle = $"Atendiendo ticket {routeParam}";
                    Result = View("~/Views/Ticket/Asignados_Detail.cshtml", mainModel);
                }
            }
            catch
            {
                Result = HttpNotFound();
            }

            return Result;
        }
    }
}