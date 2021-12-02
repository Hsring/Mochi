using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingCart.Pages.UsersControl
{
    public class UsersModel : PageModel
    {
        public void OnGet()
        {
        }
        public void Onusres1(int sessioncount)
        {
            ViewData["msg"] = $"You Query for{sessioncount} is prossed";
        }
        public void Onusres2(int sessioncount)
        {
            ViewData["msg"] = $"You Query for{sessioncount} is prossed";
        }
        public void Onusres3(int sessioncount)
        {
            ViewData["msg"] = $"You Query for{sessioncount} is prossed";
        }
    }
}
