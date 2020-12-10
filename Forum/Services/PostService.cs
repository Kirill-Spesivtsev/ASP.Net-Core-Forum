using ForumProject.Data;
using ForumProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Services
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var post = GetPostById(id);
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task ClearReplies(int id)
        {

            var post = GetPostById(id);
            _context.RemoveRange(post.Replies);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReply(int id)
        {

            var reply = GetReplyById(id);
            _context.Remove(reply);
            await _context.SaveChangesAsync();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts
                .Include(post => post.User)
                .Include(post => post.Replies).ThenInclude(reply => reply.User)
                .Include(post => post.Forum);
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies).ThenInclude(reply => reply.User)
                .Include(post => post.Forum)
                .First();
        }

        public PostReply GetReplyById(int id)
        {
            return _context.PostReplies.Where(reply => reply.Id == id)
                .Include(reply => reply.User)
                .Include(reply => reply.Post).ThenInclude(reply => reply.Forum)
                .First();
        }

        public IEnumerable<Post> GetFilteredPost(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetLatestPosts(int n)
        {
            return GetAllPosts().OrderByDescending(post => post.Created).Take(n);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            var posts = _context.Forums.Where(forum => forum.Id == id).First().Posts;
            return posts;
        }

    }
}
