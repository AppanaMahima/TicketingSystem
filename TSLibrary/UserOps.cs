using System;
using System.Collections.Generic;
using System.Linq;
using TSLibrary.Models;

namespace TSLibrary
{
    public class UserOps
    {
        TicketingSystemContext cnt = new TicketingSystemContext();

        public void CreateUser(Users user)
        {
            try
            {
                cnt.Users.Add(user);
                cnt.SaveChanges();
            }
            catch { throw new TsException("Can't have multiple records with same values"); }
        }

        public Users GetUser(string id)
        {
            try { return (from x in cnt.Users where x.UserId == id select x).First(); }
            catch { throw new TsException("No such user exists."); }
        }

        public List<Users> GetUsers()
        {
            return cnt.Users.ToList();
        }

        public List<Users> GetUsersByDept(string id)
        {
            var users = (from x in cnt.Users where x.DeptId == id select x).ToList();
            if(users!=null) { return users; }
            else { throw new TsException("No user exist."); }
        }

        public void UpdateUser(string id, Users user)
        {
            Users usr = GetUser(id);
            usr.Name = user.Name;
            usr.ContactNo = user.ContactNo;
            usr.EmailId = user.EmailId;
            usr.Password = user.Password;
            try { cnt.SaveChanges(); }
            catch { throw new TsException("Can't update, as the data is in use elsewhere."); }
        }
    }
}
