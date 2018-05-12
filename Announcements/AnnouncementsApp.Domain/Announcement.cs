using System;
using System.Collections.Generic;
using System.Text;

namespace AnnouncementsApp.Domain
{
    public class Announcement
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool Status { get; set; }

        public string Location { get; set; }

        public string ConfirmationCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Category Category { get; set; }

        public virtual User Owner { get; set; }

    }
}
