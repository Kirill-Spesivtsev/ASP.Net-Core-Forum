using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Interfaces;
using ForumProject.Models;
using ForumProject.ViewModels.ApplicationUserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace ForumProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService, IUpload uploadService, IConfiguration config, ILogger<ProfileController> logger)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _logger = logger;
            _configuration = config;
        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new UserProfileModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                MemberSince = user.MemberSince,
                IsAdmin = userRoles.Contains("Admin")
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            var connString = _configuration.GetConnectionString("AzureStorageAccount");

            _logger.Log(LogLevel.Error, $"CONN: <{connString}>");

            var container = _uploadService.GetBlobContainer(connString);

            var contentDispotition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            var fileName = contentDispotition.FileName.ToString().Trim('"');

            var blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            await _userService.SetUserProfileImage(userId, blockBlob.Uri);

            return RedirectToAction("Detail", "Profile", new {id = userId});
        }
    }
}
