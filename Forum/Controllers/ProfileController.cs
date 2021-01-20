using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Interfaces;
using ForumProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        /*
        private readonly IAplicationUser _userService;
        private readonly IUpload _uploadService;

        public ProfileController(UserManager<ApplicationUser> userManager, IAplicationUser userService, IUpload uploadService)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
        }*/

        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}
