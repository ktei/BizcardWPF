using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using LiteApp.Bizcard.Framework;
using LiteApp.Bizcard.Helpers;
using LiteApp.Bizcard.Models;
using LiteApp.Bizcard.ViewModels;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace LiteApp.Bizcard.Views
{
    public class MetroWindowEx : MetroWindow, IHandle<ThemeMessage>
    {
        public MetroWindowEx()
        {
            this.SatisfyImports();
            EventAggregator.Subscribe(this);
            this.Loaded += MetroWindowEx_Loaded;
        }

        [Import]
        public IEventAggregator EventAggregator { get; set; }

        [Import]
        public IConfiguration Configuration { get; set; }

        public void Handle(ThemeMessage message)
        {
            ChangeColor(message.Color);
        }

        void InitColor()
        {
            ChangeColor(Configuration.Color);
        }

        void MetroWindowEx_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitColor(); // Set up color as per user's choice
        }

        void ChangeColor(ThemeColor color)
        {
            switch (color)
            {
                case ThemeColor.Blue: ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.Single(x => x.Name == "Blue"), Theme.Light); break;
                case ThemeColor.Green: ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.Single(x => x.Name == "Green"), Theme.Light); break;
                case ThemeColor.Orange: ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.Single(x => x.Name == "Orange"), Theme.Light); break;
                case ThemeColor.Purple: ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.Single(x => x.Name == "Purple"), Theme.Light); break;
                case ThemeColor.Red: ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.Single(x => x.Name == "Red"), Theme.Light); break;
                default: ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.Single(x => x.Name == "Blue"), Theme.Light); break;
            }
        }
    }
}
