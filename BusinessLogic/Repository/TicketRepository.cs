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
    public class TicketRepository : Repository<Ticket>
    {
        //agregar ticket
        public RequestResponse AgregarTicket(Ticket ticket)
        {
            RequestResponse resp = new RequestResponse();

            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(ticket).State = EntityState.Added;
                    db.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    resp = new RequestResponse(ex.Message);
                }
                return resp;
            }
        }

        //actualizar ticket
        public RequestResponse ActualizarTicket(Ticket ticket)
        {
            RequestResponse resp = new RequestResponse();

            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Ticket currentTicket = db.Tickets.FirstOrDefault(t => t.Id == ticket.Id);

                    currentTicket = MapOut(currentTicket, ticket);

                    db.Entry(currentTicket).State = EntityState.Modified;
                    db.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    resp = new RequestResponse(ex.Message);
                }
                return resp;
            }
        }

        //devuelve la cantidad de tickets
        public int ObtenerCantidadTickets(MainModel mainModel, string statusTicket = null)
        {
            int ticketsCount = 0;

            try
            {
                if(statusTicket == null || string.IsNullOrEmpty(statusTicket))
                {
                    if (mainModel.Role.Equals(CustomEnums.TicketRoles.AssignTicket.ToString()))
                    {
                        ticketsCount = db.Tickets.Count();
                    }
                    else
                    {
                        ticketsCount = db.Tickets.Where(t => t.UserId.Equals(mainModel.applicationUser.Id)).Count();
                    }
                    
                }
                else
                {
                    if (mainModel.Role.Equals(CustomEnums.TicketRoles.AssignTicket.ToString()))
                    {
                        ticketsCount = db.Tickets
                        .Where(t => t.StatusTicket.Siglas.Equals(statusTicket))
                        .Count();
                    }
                    else
                    {
                        ticketsCount = db.Tickets
                        .Where(t => t.StatusTicket.Siglas.Equals(statusTicket) 
                        && t.UserId.Equals(mainModel.applicationUser.Id))
                        .Count();
                    }

                        
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return ticketsCount;
        }

        //devuelve los tickets basado en el rol
        public IEnumerable<Ticket> ObtenerTickets(MainModel mainModel, string statusTicket = null)
        {
            IEnumerable<Ticket> tickets;

            try
            {
                if (mainModel.Role.Equals(CustomEnums.TicketRoles.AssignTicket.ToString()))
                {
                    if(statusTicket == null || string.IsNullOrEmpty(statusTicket))
                    {
                        tickets = db.Tickets;
                    } else
                    {
                        tickets = db.Tickets.Where(t => t.StatusTicket.Siglas.Equals(statusTicket));
                    }  
                } 
                else
                {
                    if (statusTicket == null || string.IsNullOrEmpty(statusTicket))
                    {
                        tickets = db.Tickets.Where(t => t.UserId.Equals(mainModel.applicationUser.Id));
                    }
                    else
                    {
                        tickets = db.Tickets.Where(t => t.UserId.Equals(mainModel.applicationUser.Id) && t.StatusTicket.Siglas.Equals(statusTicket));
                    }

                    
                }
            } 
            catch(Exception ex)
            {
                tickets = null;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return tickets;
        }

        //devuelve un ticket por el codigo
        public Ticket ObtenerTicketPorCodigo(string userId, string codigoTicket)
        {
            return db.Tickets.FirstOrDefault(t => t.UserId.Equals(userId) && t.CodigoTicket.Equals(codigoTicket));
        }

        //devuelve un ticket por el codigo
        public Ticket ObtenerTicketPorId(int ticketId)
        {
            return db.Tickets.FirstOrDefault(t => t.Id == ticketId);
        }
    }
}
