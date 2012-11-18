using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using LiteApp.Bizcard.ViewModels;
using LiteApp.Bizcard.Resources;

namespace LiteApp.Bizcard.Views
{
    /// <summary>
    /// Interaction logic for MessageView.xaml
    /// </summary>
    public partial class MessageView : MetroWindowEx
    {
        MessageViewModel _messageViewModel;

        public MessageView()
        {
            InitializeComponent();
            this.Loaded += MessageView_Loaded;
            this.Unloaded += MessageView_Unloaded;
        }

        void MessageView_Loaded(object sender, RoutedEventArgs e)
        {
            _messageViewModel = (MessageViewModel)this.DataContext;
            _messageViewModel.PropertyChanged += _messageViewModel_PropertyChanged;
            UpdateButtons();
            UpdateIcon();
        }

        void _messageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ButtonOptions")
            {
                UpdateButtons();   
            }
            else if (e.PropertyName == "Icon")
            {
                UpdateIcon();
            }
        }

        void MessageView_Unloaded(object sender, RoutedEventArgs e)
        {
            _messageViewModel.PropertyChanged -= _messageViewModel_PropertyChanged;
        }

        void UpdateButtons()
        {
            PositiveButton.Visibility = System.Windows.Visibility.Collapsed;
            NegativeButton.Visibility = System.Windows.Visibility.Collapsed;
            if (_messageViewModel.ButtonOptions == MessageViewModel.Buttons.OK)
            {
                PositiveButton.Content = ApplicationStrings.OkayLabel;
                PositiveButton.Visibility = System.Windows.Visibility.Visible;
            }
            else if (_messageViewModel.ButtonOptions == MessageViewModel.Buttons.OKCancel)
            {
                PositiveButton.Content = ApplicationStrings.OkayLabel;
                PositiveButton.Visibility = System.Windows.Visibility.Visible;
                NegativeButton.Content = ApplicationStrings.CancelLabel;
                NegativeButton.Visibility = System.Windows.Visibility.Visible;
            }
            else if (_messageViewModel.ButtonOptions == MessageViewModel.Buttons.YesNo)
            {
                PositiveButton.Content = ApplicationStrings.YesLabel;
                PositiveButton.Visibility = System.Windows.Visibility.Visible;
                NegativeButton.Content = ApplicationStrings.NoLabel;
                NegativeButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        void UpdateIcon()
        {
            Exclamation.Visibility = System.Windows.Visibility.Collapsed;
            Question.Visibility = System.Windows.Visibility.Collapsed;
            if (_messageViewModel.Icon == MessageViewModel.HeaderIcon.Exclamation)
            {
                Exclamation.Visibility = System.Windows.Visibility.Visible;
            }
            else if (_messageViewModel.Icon == MessageViewModel.HeaderIcon.Question)
            {
                Question.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void PositiveButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
