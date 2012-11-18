using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.Data
{
    public interface IUserSettingsRepository : IRepository
    {
        UserSettings Get();
        UserSettings Reload(int id);
        void Save(UserSettings settings);
    }
}
