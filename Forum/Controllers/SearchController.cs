using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Data;
using ForumProject.Models;
using ForumProject.Models.ForumModels;
using ForumProject.Models.PostModels;
using ForumProject.Models.SearchModels;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPost _postService;

        public SearchController(IPost postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchQuery) 
        {
            return RedirectToAction("SearchResults", new { searchQuery });
        }

        public IActionResult SearchResults(string searchQuery) 
        {
            var posts = _postService.GetFilteredPosts(searchQuery).ToList();

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                Forum = BuildForumListing(post),
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count()
            }).OrderByDescending(post => post.DatePosted);

            var areAnyResults = string.IsNullOrWhiteSpace(searchQuery) || posts.Any();

            var model = new SearchResultModel
            {
                IsResultEmpty = !areAnyResults,
                Posts = postListings,
                SearchQuery = searchQuery,
            };

            return View(model);
        }

         

        private static ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                ImageUrl = forum.ImageUrl,
                Name = forum.Title,
                Description = forum.Description
            };
        }

        private static ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }
    }

}
