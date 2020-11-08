using ForumProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Data
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPost(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);
        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
        Task AddReply(PostReply reply);
    }
}
