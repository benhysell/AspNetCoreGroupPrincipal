using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetGroupPrincipal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var pc = new PrincipalContext(ContextType.Domain, "YOUR_DOMAIN_HERE", "USERNAME", "PW"))
            {
                SortedSet<string> ldapResults;
                using (var gp = GroupPrincipal.FindByIdentity(pc, IdentityType.Name, "GROUP_TO_LOOKUP"))
                {
                    ldapResults = gp == null ? null : new SortedSet<string>(gp.GetMembers(true).Select(u => u.SamAccountName));
                }
            }
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
