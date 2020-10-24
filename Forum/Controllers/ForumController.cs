using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum forumService;

        public ForumController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
