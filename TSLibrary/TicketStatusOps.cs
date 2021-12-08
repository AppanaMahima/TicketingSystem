using System;
using System.Collections.Generic;
using System.Linq;
using TSLibrary.Models;

namespace TSLibrary
{
    public class TicketStatusOps
    {
        TicketingSystemContext cnt = new TicketingSystemContext();
        public TicketStatus GetTicketStatus(string id)
        {
            try { return (from x in cnt.TicketStatus where x.StatusId == id select x).First(); }
            catch { throw new TsException("No such Ticket Status exists."); }
        }

        public List<TicketStatus> GetTicketStatuses()
        {
            return cnt.TicketStatus.ToList();
        }
    }
}
