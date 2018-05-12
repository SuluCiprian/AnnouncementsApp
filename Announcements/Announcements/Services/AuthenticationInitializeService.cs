using Announcements.Data;
using Announcements.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcements.Services
{
    public class AuthenticationInitializeService : IAuthenticationInitializeService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthenticationInitializeService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async void Initialize()
        {
            //create database schema if none exists
            context.Database.EnsureCreated();

            //Create the Administartor Role
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            //Create the default Admin account and apply the Administrator role
            string user = "admin@gmail.com";
            string password = "Parola1!";

            if (!context.Users.Any(usr => usr.UserName.Equals(user)))
            {
                ApplicationUser appUser = new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true };
                var result = await userManager.CreateAsync(appUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, "Admin");
                }
            }

        }
    }
}
