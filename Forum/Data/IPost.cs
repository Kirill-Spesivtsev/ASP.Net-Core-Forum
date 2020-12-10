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
        IEnumerable<Post> GetFilteredPost(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        IEnumerable<Post> GetLatestPosts(int n);

        Post GetPostById(int id);
        PostReply GetReplyById(int id);
        Task Add(Post post);
        Task DeletePost(int id);
        Task DeleteReply(int id);
        Task ClearReplies(int id);
        Task EditPostContent(int id, string newContent);
        Task AddReply(PostReply reply);

        
    }
}
