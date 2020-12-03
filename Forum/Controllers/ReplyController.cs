using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Data;
using ForumProject.Models;
using ForumProject.Models.ReplyModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class ReplyController : Controller
    {

        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReplyController(IPost postService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Create(int id)
        {
            var post = _postService.GetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,

                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorId = user.Id,
                AuthorRating = user.Rating,
                //IsAuthorAdmin = user.IsInRole("Admin"),
                Created = DateTime.Now,

                ForumName = post.Forum.Title,
                ForumId = post.Forum.Id,
                ForumImageUrl = post.Forum.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);
            await _postService.AddReply(reply);
            //await _userService.BumpRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }

        private PostReply BuildReply(PostReplyModel reply, ApplicationUser user)
        {
            var now = DateTime.Now;
            var post = _postService.GetById(reply.PostId);

            return new PostReply
            {
                Post = post,
                Content = reply.ReplyContent,
                Created = now,
                User = user
            };
        }
    }
}
