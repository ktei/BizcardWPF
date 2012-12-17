using System.ComponentModel.Composition;
using Caliburn.Micro;
using LiteApp.Bizcard.Framework;
using LiteApp.Bizcard.Helpers;
using LiteApp.Bizcard.Models;
using LiteApp.Bizcard.Resources;
using MahApps.Metro;

namespace LiteApp.Bizcard.ViewModels
{
    public class SettingsViewModel : Conductor<ThemeViewModel>.Collection.OneActive
    {
        public SettingsViewModel()
        {
            DisplayName = ApplicationStrings.SettingsLabel;
            this.SatisfyImports();
        }

        [Import]
        public IEventAggregator EventAggregator { get; set; }

        [Import]
        public IConfiguration Configuration { get; set; }

        public void Save()
        {
            Configuration.Save();
        }

        public void Cancel()
        {
            Configuration.Rollback();
            EventAggregator.Publish(new ThemeMessage(Configuration.Color));
        }

        protected override void OnInitialize()
        {
            LoadThemes();
            base.OnInitialize();
        }

        public override void ActivateItem(ThemeViewModel item)
        {
            base.ActivateItem(item);
            if (item != null)
            {
                EventAggregator.Publish(new ThemeMessage(item.Color));
                Configuration.Color = item.Color;
            }
        }

        void LoadThemes()
        {
            foreach (var a in ThemeManager.DefaultAccents)
            {
                Items.Add(new ThemeViewModel(MapToColor(a.Name)));
            }
        }

        ThemeColor MapToColor(string accentName)
        {
            switch (accentName)
            {
                case "Blue": return ThemeColor.Blue;
                case "Green": return ThemeColor.Green;
                case "Orange": return ThemeColor.Orange;
                case "Purple": return ThemeColor.Purple;
                case "Red": return ThemeColor.Red;
                default: return ThemeColor.Blue;
            }
        }
    }
}
