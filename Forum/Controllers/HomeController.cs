using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ForumProject.Models;
using ForumProject.ViewModels.HomeModels;
using ForumProject.Interfaces;
using ForumProject.ViewModels.PostModels;
using ForumProject.ViewModels.ForumModels;

namespace ForumProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IPost _postService;

        public HomeController(ILogger<HomeController> logger, IPost postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var model = BuildHomeIndexModel();
            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var latestPosts = _postService.GetLatestPosts(10);
            var posts = latestPosts.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                Forum = BuildForumListingForPost(post)
            });

            return new HomeIndexModel
            {
                Latestposts = posts,
                SearchQuery = ""
            };
        }

        private ForumListingModel BuildForumListingForPost(Post post)
        {
            var forum = post.Forum;

            return new ForumListingModel 
            {
                Id = forum.Id,
                Name = forum.Title,
                ImageUrl = forum.ImageUrl
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
