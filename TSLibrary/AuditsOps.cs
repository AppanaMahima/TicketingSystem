using System;
using System.Collections.Generic;
using TSLibrary.Models;
using System.Linq;

namespace TSLibrary
{
    public class AuditsOps
    {
        TicketingSystemContext cnt = new TicketingSystemContext();

        public void CreateAudit(Audits audit)
        {
            try
            {
                cnt.Audits.Add(audit);
                TicketOps top = new TicketOps();
                Ticket t = top.GetTicket((int)audit.TicketId);
                t.StatusId = audit.StatusId;
                cnt.SaveChanges();
            }
            catch { throw new TsException("Can't have multiple records with same values"); }
        }

        public List<Audits> GetAuditsByUser(string id)
        {
            var audits = from x in cnt.Audits where x.AssignedTo == id select x;
            if (audits != null) { return audits.ToList(); }
            else { throw new TsException("No such user exists ."); }
        }

        public List<Audits> GetAuditsByTicket(int id)
        {
            var audits = from x in cnt.Audits where x.TicketId == id select x;
            if (audits != null) { return audits.ToList(); }
            else { throw new TsException("No such ticket exists ."); }
        }

        public List<Audits> GetAudits()
        {
            return cnt.Audits.ToList();
        }
    }
}
