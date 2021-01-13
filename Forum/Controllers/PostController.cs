using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ForumProject.Data;
using ForumProject.Models.ReplyModels;
using ForumProject.Models.PostModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ForumProject.Models;

namespace ForumProject.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private static UserManager<ApplicationUser> _userManager;
        public PostController(IPost postService, IForum forumService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var post = _postService.GetPostById(id);
            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Replies = replies,
                PostRating = post.Rating,
                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title,
                IsAuthorAdmin = IsAuthorAdmin(post.User),
            };

            return View(model);
        }

        
        
        
        public IActionResult Create(int forumId)
        {
            var forum = _forumService.GetById(forumId);
            var model = new NewPostModel 
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };
            
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model) 
        {
            if (ModelState.IsValid) 
            {
                var userId = _userManager.GetUserId(User);
                var user = _userManager.FindByIdAsync(userId).Result;
                var post = BuildPost(model, user);
                model.ForumName = post.Forum.Title;
                post.User.Rating++;
                await _postService.AddPost(post);
                return RedirectToAction("Index", "Post", new {id = post.Id });
            }
            return Create(model.ForumId);
        }

        public IActionResult Edit(int postId)
        {
            var post = _postService.GetPostById(postId);
            var forum = _forumService.GetById(post.Forum.Id);
            var model = new NewPostModel
            {
                Title = post.Title,
                Content = post.Content,
                Id = postId,
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl
            };

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(NewPostModel model)
        {
            if (ModelState.IsValid)
            {
                await _postService.EditPost(model.Id, model.Title, model.Content);
                return RedirectToAction("Index", "Post", new { id = model.Id });
            }
            return Edit(model.Id);
        }



        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id) 
        {
            var post = _postService.GetPostById(id);
            await _postService.ClearReplies(id);
            await _postService.DeletePost(id);
            return RedirectToAction("Index", "Forum", new { id = post.Forum.Id });
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _forumService.GetById(model.ForumId);
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Forum = forum
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<Models.PostReply> replies)
        {
            return replies.OrderByDescending(reply => reply.Created).Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content,
                ReplyRating = reply.Rating,
                PostId = reply.Post.Id, //
                IsAuthorAdmin = IsAuthorAdmin(reply.User)
            });
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user).Result.Contains("Admin");
        }

    }
}
