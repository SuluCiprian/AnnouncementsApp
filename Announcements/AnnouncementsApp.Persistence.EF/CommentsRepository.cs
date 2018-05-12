using AnnouncementsApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnnouncementsApp.Persistence.EF
{
    public class CommentsRepository: Repository<Comment>, ICommentsRepository
    {
        public CommentsRepository(AnnouncementsContext context)
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
    }
}
