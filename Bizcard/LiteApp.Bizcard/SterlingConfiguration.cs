using System.ComponentModel.Composition;
using LiteApp.Bizcard.Data;
using LiteApp.Bizcard.Framework;
using LiteApp.Bizcard.Models;
using System.IO;
using System;

namespace LiteApp.Bizcard
{
    [Export(typeof(IConfiguration))]
    public class SterlingConfiguration : IConfiguration
    {
        IUserSettingsRepository _userSettingsRepository;
        UserSettings _settings;

        [Import]
        public RepositoryFactory RepositoryFactory { get; set; }

        IUserSettingsRepository UserSettingsRepository
        {
            get
            {
                if (_userSettingsRepository == null)
                {
                    _userSettingsRepository = RepositoryFactory.GetRepository<IUserSettingsRepository>();
                }
                return _userSettingsRepository;
            }
        }

        public DataSource DataSource
        {
            get { return DataSource.Sterling; }
        }

        public ThemeColor Color
        {
            get { return Settings.Color; }
            set { Settings.Color = value; }
        }

        private UserSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    Load();
                }
                return _settings;
            }
        }

        public void Save()
        {
            UserSettingsRepository.Save(_settings);
        }

        public void Rollback()
        {
            try
            {
                _settings = UserSettingsRepository.Reload(_settings.Id);
                if (_settings == null) // No settings found in database, so create default
                {
                    LoadDefault();
                }
            }
            catch
            {
                LoadDefault();
            }
        }

        void Load()
        {
            try
            {
                _settings = UserSettingsRepository.Get();
                if (_settings == null) // No settings found in database, so create default
                {
                    LoadDefault();
                }
            }
            catch
            {
                LoadDefault();
            }
        }

        void LoadDefault()
        {
            _settings = new UserSettings();
            _settings.Color = ThemeColor.Blue;
            Save(); // Save default by the way
        }


        public string LogFolderPath
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bizcard", "Log"); }
        }
    }
}
