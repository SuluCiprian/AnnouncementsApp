using AnnouncementsApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnnouncementsApp.Persistence.EF
{
    public class AnnouncementsContext: DbContext
    {
        public AnnouncementsContext(DbContextOptions<AnnouncementsContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
