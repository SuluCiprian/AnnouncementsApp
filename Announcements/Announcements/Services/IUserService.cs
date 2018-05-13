using Announcements.Models;
using AnnouncementsApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcements.Services
{
    public interface IUserService
    {
        void CreateUser(User teacher);
        int GetUserId(string userName);

        IEnumerable<User> GetUsers();
        User GetSignedInUser();
        Task<bool> IsAdmin();
    }
}
