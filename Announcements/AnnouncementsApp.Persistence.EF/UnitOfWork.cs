using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnouncementsApp.Persistence.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private AnnouncementsContext _context = null;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            InitializeDbContext(serviceProvider);
        }

        public IAnnouncementsRepository Announcements { get; set; }

        public ICategoriesRepository Categories { get; set; }

        public IUserRepository Users { get; set; }

        public ICommentsRepository Comments { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        private void InitializeDbContext(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<AnnouncementsContext>();
            if (_context != null)
            {
                Announcements = new AnnouncementsRepository(_context);
                Categories = new CategoriesRepository(_context);
                Users = new UserRepository(_context);
            }
        }

        public void InitializeData(IServiceProvider serviceProvider)
        {

            if (!_context.Categories.Any(at => at.Name.Equals("Masini")))
                _context.Categories.Add(new Domain.Category { Name = "Masini" });

            if (!_context.Categories.Any(at => at.Name.Equals("Case")))
                _context.Categories.Add(new Domain.Category { Name = "Case" });

            //if (!_context.Users.Any(at => at.UserName.Equals("test@test.com")))
            //    _context.Users.Add(new Domain.User { UserName = "test@test.com", Name = "Test Teston Jr." });
            //_context.SaveChanges();

            if (!_context.Users.Any(at => at.UserName.Equals("teacher@gmail.com")))
                _context.Users.Add(new Domain.User { UserName = "teacher@gmail.com", FirstName = "Buffalo" });
            _context.SaveChanges();

        }

        public void InitializeContext(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AnnouncementsContext>(options =>
                                     options.UseLazyLoadingProxies()
                                     .UseSqlServer(config.GetConnectionString("AnnouncementsApp")));

            InitializeDbContext(services.BuildServiceProvider());
        }
    }
}
