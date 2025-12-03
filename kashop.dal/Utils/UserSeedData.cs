using kashop.dal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Utils
{
    public class UserSeedData : ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSeedData(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task DataSeed()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser
                {
                    UserName = "Jannat",
                    Email = "j@gmail.com",
                    FullName = "Jannat Dawabsheh",
                    EmailConfirmed = true

                };
                var user2 = new ApplicationUser
                {
                    UserName = "sama",
                    Email = "s@gmail.com",
                    FullName = "sama Dawabsheh",
                    EmailConfirmed = true

                };
                var user3 = new ApplicationUser
                {
                    UserName = "huda",
                    Email = "h@gmail.com",
                    FullName = "huda Dawabsheh",
                    EmailConfirmed = true

                };

                await _userManager.CreateAsync(user1,"Pass@1122");
                await _userManager.CreateAsync(user2, "Pass@1122");
                await _userManager.CreateAsync(user3, "Pass@1122");

                await _userManager.AddToRoleAsync(user1, "SuperAdmin");
                await _userManager.AddToRoleAsync(user2, "Admin");
                await _userManager.AddToRoleAsync(user3, "User");

            }


        }
    }
}
