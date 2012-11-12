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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiteApp.Bizcard.ViewModels;

namespace LiteApp.Bizcard.Views
{
    /// <summary>
    /// Interaction logic for ContactsView.xaml
    /// </summary>
    public partial class ContactsWorkspaceView : UserControl
    {
        public ContactsWorkspaceView()
        {
            InitializeComponent();
        }

        private void SearchBox_Search(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ContactsWorkspaceViewModel).Search(SearchBox.Text.Trim());
        }
    }
}
