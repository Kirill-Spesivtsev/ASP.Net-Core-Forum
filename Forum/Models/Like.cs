using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
