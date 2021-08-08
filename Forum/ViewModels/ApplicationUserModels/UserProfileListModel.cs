using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.ViewModels.ApplicationUserModels
{
    public class UserProfileListModel
    {
        public IEnumerable<UserProfileModel> Profiles { get; set; }
    }
}
