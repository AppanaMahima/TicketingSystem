using System;
using System.Collections.Generic;
using System.Linq;
using TSLibrary.Models;

namespace TSLibrary
{
    public class CategoriesOps
    {
        TicketingSystemContext cnt = new TicketingSystemContext();

        public Categories GetCategory(string ctgid)
        {
            try { return (from x in cnt.Categories where x.CtgId == ctgid select x).First(); }
            catch { throw new TsException("No such category exists."); }
        }

        public List<Categories> GetCategoriesByDept(string deptid)
        {
            try { return (from x in cnt.Categories where x.DeptId == deptid select x).ToList(); }
            catch { throw new TsException("No such department exists."); }
        }

        public List<Categories> GetCategories()
        {
            return cnt.Categories.ToList();
        }
    }
}
