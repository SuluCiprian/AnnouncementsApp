﻿using AnnouncementsApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnnouncementsApp.Persistence
{
    public interface IAnnouncementsRepository: IRepository<Announcement>
    {
    }
}
