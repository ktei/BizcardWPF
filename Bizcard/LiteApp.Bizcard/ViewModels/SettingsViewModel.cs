using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using MahApps.Metro;
using System.ComponentModel.Composition;
using LiteApp.Bizcard.Resources;

namespace LiteApp.Bizcard.ViewModels
{
    [Export]
    public class SettingsViewModel : Conductor<ThemeViewModel>.Collection.OneActive
    {
        public SettingsViewModel()
        {
            DisplayName = ApplicationStrings.SettingsLabel;
        }

        protected override void OnInitialize()
        {
            LoadThemes();
            base.OnInitialize();
        }

        public override void ActivateItem(ThemeViewModel item)
        {
            base.ActivateItem(item);
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
