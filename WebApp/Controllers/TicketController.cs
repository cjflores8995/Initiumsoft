using BusinessLogic.Common;
using BusinessLogic.Repository;
using Microsoft.AspNet.Identity;
using Models.Models;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace WebApp.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        MainModel mainModel = new MainModel();

        public TicketController()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            using (var context = new ApplicationDbContext())
            {
                mainModel.applicationUser = context.Users.Find(userId);
                mainModel.Role = new UserRepository().GetRoleUser(userId);
            }
        }

        public ActionResult Index()
        {
            mainModel.PageTitle = "Inicio";

            ViewBag.TicketsTotal = new TicketRepository().ObtenerCantidadTickets(mainModel);
            ViewBag.TicketsAsignados = new TicketRepository().ObtenerCantidadTickets(mainModel, StatusTicket.EnumEstatusTicket.asignado.ToString());
            ViewBag.TicketsAtendidos = new TicketRepository().ObtenerCantidadTickets(mainModel, StatusTicket.EnumEstatusTicket.atendido.ToString());

            return View(mainModel);
        }

        [Authorize(Roles = "AssignTicket")]
        public ActionResult Ingresar()
        {
            mainModel.PageTitle = "Ingresar ticket";

            mainModel.Ticket = new Ticket
            {
                CodigoTicket = Helpers.GenerateIdentifier()
            };
            mainModel.ApplicationUsers = new UserRepository().GetAllUsers(CustomEnums.TicketRoles.SolveTicket.ToString()).ToList();

            ViewBag.CustomUserFields = mainModel.ApplicationUsers
                .Select(x => new { Id = x.Id, FirstName = string.Format($"{x.FirstName} {x.LastName} - {x.Email}") })
                .ToList();

            ViewBag.ListadoUsuarios = new SelectList(ViewBag.CustomUserFields, "Id", "FirstName");

            ViewBag.Queue = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Cola 1", Value = "1"},
            new SelectListItem() { Text = "Cola 2", Value ="2"},
            };

            return View(mainModel);
        }

        public ActionResult Asignados(string Id = null)
        {
            ActionResult Result;

            Result = new RouteController().HandleAssignedRoute(mainModel, Id);

            return Result;
        }

        public ActionResult Atendidos()
        {
            mainModel.PageTitle = "Tickets atentidos";
            mainModel.Tickets = new TicketRepository().ObtenerTickets(mainModel, StatusTicket.EnumEstatusTicket.atendido.ToString()).ToList();

            return View(mainModel);
        }
    }
}