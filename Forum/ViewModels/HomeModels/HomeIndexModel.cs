using ForumProject.ViewModels.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.ViewModels.HomeModels
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingModel> Latestposts { get; set; }

    }
}
