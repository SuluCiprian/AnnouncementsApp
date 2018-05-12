using System;
using System.Collections.Generic;
using System.Text;

namespace AnnouncementsApp.Domain
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
