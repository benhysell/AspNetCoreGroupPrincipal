using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreGroupPrincipal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (var pc = new PrincipalContext(ContextType.Domain, "YOUR_DOMAIN_HERE", "USERNAME", "PW"))
            {
                SortedSet<string> ldapResults;
                using (var gp = GroupPrincipal.FindByIdentity(pc, IdentityType.Name, "GROUP_TO_LOOKUP"))
                {
                    ldapResults = gp == null ? null : new SortedSet<string>(gp.GetMembers(true).Select(u => u.SamAccountName));
                }
            }
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
