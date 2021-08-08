using ForumProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Data
{
    public class DataSeeder
    {
        private ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task SeedAdmin() 
        {
            var user = new ApplicationUser()
            {

            };
        }
    }
}
