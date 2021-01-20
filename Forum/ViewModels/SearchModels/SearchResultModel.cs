using ForumProject.ViewModels.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.ViewModels.SearchModels
{
    public class SearchResultModel
    {
        public IEnumerable<PostListingModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool IsResultEmpty { get; set; }
    }
}
