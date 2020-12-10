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

        public async Task<IActionResult> Create(int postId)
        {
            var post = _postService.GetPostById(postId);
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

            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyModel model)
        {
            if (ModelState.IsValid) 
            {
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);
                var reply = BuildReply(model, user);
                model.ForumName = reply.Post.Forum.Title;
                await _postService.AddReply(reply);
                return RedirectToAction("Index", "Post", new { id = model.PostId });
            }

            return await Create(model.PostId);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var reply = _postService.GetReplyById(id);
            await _postService.DeleteReply(id);
            return RedirectToAction("Index", "Post", new { id = reply.Post.Id });
        }

        private PostReply BuildReply(PostReplyModel reply, ApplicationUser user)
        {
            var now = DateTime.Now;
            var post = _postService.GetPostById(reply.PostId);

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
