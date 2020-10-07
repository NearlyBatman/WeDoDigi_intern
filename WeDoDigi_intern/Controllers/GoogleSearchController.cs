using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeDoDigi_intern.Controllers
{
    public class GoogleSearchController : Controller
    {
        public IActionResult SearchPosts()
        {
            return View();
        }
    }
}
