using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnnouncementsApp.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IAnnouncementsRepository Announcements { get; }
        IUserRepository Users { get; }
        ICategoriesRepository Categories { get; }
        ICommentsRepository Comments { get; }
        int Complete();
        void InitializeContext(IServiceCollection services, IConfiguration config);
        void InitializeData(IServiceProvider serviceProvider);
    }
}
