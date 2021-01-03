using ForumProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Data
{
    public interface IPost
    {
        
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Post> GetFilteredPosts(int forumid, string searchQuery);
        IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLatestPosts(int n);

        Post GetPostById(int id);
        PostReply GetReplyById(int id);
        Task AddPost(Post post);
        Task EditPost(int id, string title, string content);
        Task EditReply(int id, string content);
        Task DeletePost(int id);
        Task DeleteReply(int id);
        Task ClearReplies(int id);
        Task AddReply(PostReply reply);

        
    }
}
