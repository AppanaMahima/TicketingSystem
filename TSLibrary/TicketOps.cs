using System;
using System.Collections.Generic;
using System.Linq;
using TSLibrary.Models;

namespace TSLibrary
{
    public class TicketOps
    {
        TicketingSystemContext cnt = new TicketingSystemContext();

        public void CreateTicket(Ticket ticket)    
        {
            try
            {
                AuditsOps aOp = new AuditsOps();
                Audits aud = new Audits();
                cnt.Ticket.Add(ticket);
                cnt.SaveChanges();
                aud.TicketId = ticket.TicketId;
                aud.AssignedTo = "U001";
                aud.StatusId = ticket.StatusId;
                aud.Remarks = "New Ticket is Created";
                aOp.CreateAudit(aud);

            }
            catch { throw new TsException("Can't have multiple records with same values"); }
        }

        public Ticket GetTicket(int id)
        {
            try { return (from x in cnt.Ticket where x.TicketId == id select x).First(); }
            catch { throw new TsException("No such ticket exists."); }
        }

        public List<Ticket> GetTickets()
        {
            return cnt.Ticket.ToList();
        }

        public List<Ticket> GetTicketsByDept(string id)
        {
            var tickets = (from x in cnt.Ticket where x.Categories.Department.DeptId == id select x).ToList();
            if(tickets!=null) { return tickets; }
            else { throw new TsException("No such ticket exists."); }
        }

        public List<Ticket> TicketsRaised(string id)
        {
            var tickets = (from x in cnt.Ticket where x.UserId == id select x).ToList();
            if (tickets!=null) { return tickets; }
            else { throw new TsException("No such ticket raised"); }
        }

        public List<Ticket> GetTicketsByStatus(string id)
        {
            var tickets = (from x in cnt.Ticket where x.TicketStatus.StatusId == id select x).ToList();
            if (tickets!=null) { return tickets; }
            else { throw new TsException("No such ticket exists"); }
        }

        public List<Ticket> GetTicketsByCategories(string id)
        {
            var tickets = (from x in cnt.Ticket where x.Categories.CtgId == id select x).ToList();
            if (tickets!=null) { return tickets; }
            else { throw new TsException("No such ticket exists"); }
        }

        public List<Ticket> GetTicketsByIssueDates(DateTime date)
        {
            var tickets = (from x in cnt.Ticket where x.IssueDate == date select x).ToList();
            if(tickets!=null) { return tickets; }
            else { throw new TsException("No such ticket exists") ; }
        }

        public List<Ticket> TicketsAssigned(string usrid)
        {
            List<Ticket> tickets = new List<Ticket>();
            AuditsOps aop = new AuditsOps();
            var audit = aop.GetAuditsByUser(usrid);
            foreach (var aud in audit)
            {
                if (aud.StatusId == "O")
                {
                    Ticket tic = GetTicket((int)aud.TicketId);
                    tickets.Add(tic);
                }
            }
            //foreach (var tkt in cnt.Ticket)
            //{
            //    var audits = aop.GetAuditsByTicket(tkt.TicketId);
            //    int lastindex = audits.Count - 1;
            //    if (audits[lastindex].AssignedTo == usrid) { tickets.Add(tkt); }
            //}
            //if (tickets != null) { return tickets; }
            //else { throw new TsException("No tickets have been assigned to you.") ; }
            return tickets;
        }
    }
}
