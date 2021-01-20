using ForumProject.ViewModels.ForumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.ViewModels.PostModels
{
    public class PostListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PostRating { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }
        public string DatePosted { get; set; }
        public ForumListingModel Forum { get; set; }
        public int RepliesCount { get; set; }
    }
}
