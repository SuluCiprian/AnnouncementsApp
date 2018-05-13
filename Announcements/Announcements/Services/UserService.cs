using Announcements.Models;
using AnnouncementsApp.Domain;
using AnnouncementsApp.Persistence;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Announcements.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserService(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            this.unitOfWork = unitOfWork;
            this.signInManager = signInManager;
        }

        public void CreateUser(User user)
        {
            unitOfWork.Users.Insert(user);
            unitOfWork.Complete();
        }

        public int GetUserId(string userName)
        {
            var user = unitOfWork.Users.GetUserWithUserName(userName);
            return user.Id;
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = unitOfWork.Users.GetAll();
            return users;
        }

        public bool IsUserSignedIn()
        {
            bool retVal = false;
            if (signInManager.IsSignedIn(signInManager.Context.User))
            {
                retVal = true;
            }
            return retVal;
        }

        public string GetUserName()
        {
            return signInManager.UserManager.GetUserName(signInManager.Context.User);
        }
        

        public async Task<bool> IsAdmin()
        {
            var id = signInManager.UserManager.GetUserId(signInManager.Context.User);
            var user = await signInManager.UserManager.FindByIdAsync(id);
            var retVal =  await signInManager.UserManager.IsInRoleAsync(user, "Admin");
            return retVal;
        }

        public User GetSignedInUser()
        {
            var userName = GetUserName();
            var id = GetUserId(userName);
            return unitOfWork.Users.GetById(id);
        }
    }
}
