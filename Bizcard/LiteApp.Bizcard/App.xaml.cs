using System;
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using LiteApp.Bizcard.Framework;
using LiteApp.Bizcard.Helpers;
using LiteApp.Bizcard.Models;
using LiteApp.Bizcard.ViewModels;

namespace LiteApp.Bizcard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IHandle<ThemeMessage>
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.SatisfyImports();
            EventAggregator.Subscribe(this);
            ChangeColor(Configuration.Color); // Set up color
        }

        [Import]
        public IEventAggregator EventAggregator { get; set; }

        [Import]
        public IConfiguration Configuration { get; set; }

        public void Handle(ThemeMessage message)
        {
            // Change our own colors
            ChangeColor(message.Color);
        }

        void ChangeColor(ThemeColor color)
        {
            this.Resources.MergedDictionaries.RemoveAt(1);
            this.Resources.MergedDictionaries.Insert(1, GetResxDictByThemeColor(color));
        }

        ResourceDictionary GetResxDictByThemeColor(ThemeColor color)
        {
            switch (color)
            {
                case ThemeColor.Blue: return new ResourceDictionary() { Source = new Uri("/LiteApp.Bizcard;component/Views/Themes/BlueThemeStyles.xaml", UriKind.Relative) };
                case ThemeColor.Green: return new ResourceDictionary() { Source = new Uri("/LiteApp.Bizcard;component/Views/Themes/GreenThemeStyles.xaml", UriKind.Relative) };
                case ThemeColor.Orange: return new ResourceDictionary() { Source = new Uri("/LiteApp.Bizcard;component/Views/Themes/OrangeThemeStyles.xaml", UriKind.Relative) };
                case ThemeColor.Purple: return new ResourceDictionary() { Source = new Uri("/LiteApp.Bizcard;component/Views/Themes/PurpleThemeStyles.xaml", UriKind.Relative) };
                case ThemeColor.Red: return new ResourceDictionary() { Source = new Uri("/LiteApp.Bizcard;component/Views/Themes/RedThemeStyles.xaml", UriKind.Relative) };
                default: return new ResourceDictionary() { Source = new Uri("/LiteApp.Bizcard;component/Views/Themes/BlueThemeStyles.xaml", UriKind.Relative) };
            }
        }

    }
}
