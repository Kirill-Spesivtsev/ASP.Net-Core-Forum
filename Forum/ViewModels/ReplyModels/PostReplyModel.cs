using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.ViewModels.ReplyModels
{
    public class PostReplyModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public int AuthorRating { get; set; }
        public int ReplyRating { get; set; }
        public bool IsAuthorAdmin { get; set; }
        public DateTime Created { get; set; }

        [Required]
        public string ReplyContent { get; set; }

        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }

        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public string ForumImageUrl { get; set; }
        

    }
}
