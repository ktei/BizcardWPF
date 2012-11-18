using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteApp.Bizcard.Models;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Framework;

namespace LiteApp.Bizcard.Data.Sterling
{
    [Export(typeof(IRepository))]
    [ExportMetadata("DataSource", DataSource.Sterling)]
    [ExportMetadata("Contract", typeof(IUserSettingsRepository))]
    public class UserSettingsRepository : IUserSettingsRepository
    {
        public UserSettings Get()
        {
            var settings = SterlingService.Current.Database.Query<UserSettings, int>().FirstOrDefault();
            if (settings != null)
                return settings.LazyValue.Value;
            return null;
        }

        public void Save(UserSettings settings)
        {
            SterlingService.Current.Database.Save(settings);
            SterlingService.Current.Database.Flush();
        }


        public UserSettings Reload(int id)
        {
            var settings = SterlingService.Current.Database.Load(typeof(UserSettings), id);
            return settings as UserSettings;
        }
    }
}
