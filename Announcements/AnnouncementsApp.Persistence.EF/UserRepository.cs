using AnnouncementsApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnouncementsApp.Persistence.EF
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(AnnouncementsContext context)
            : base(context)
        {

        }

        public AnnouncementsContext AnnouncementsContext
        {
            get
            {
                return Context as AnnouncementsContext;
            }
        }

        public User GetUserWithUserName(string userName)
        {
            var user = AnnouncementsContext.Users.FirstOrDefault(s => s.UserName == userName);
            return user;
        }
    }
}
