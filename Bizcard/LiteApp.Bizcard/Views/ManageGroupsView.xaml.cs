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
using MahApps.Metro.Controls;
using LiteApp.Bizcard.Helpers;
using LiteApp.Bizcard.ViewModels;

namespace LiteApp.Bizcard.Views
{
    /// <summary>
    /// Interaction logic for ManageGroupView.xaml
    /// </summary>
    public partial class ManageGroupsView : MetroWindow
    {
        public ManageGroupsView()
        {
            InitializeComponent();
            this.Loaded += ManageGroupsView_Loaded;
            this.Unloaded += ManageGroupsView_Unloaded;
        }


        void ManageGroupsView_Loaded(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as ManageGroupsViewModel;
            if (model != null)
            {
                model.RequestSave += model_RequestSave;
                model.RequestRollback += model_RequestRollback;
            }
        }

        void model_RequestRollback(object sender, EventArgs e)
        {
            this.DialogResult = false;
        }

        void model_RequestSave(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        void ManageGroupsView_Unloaded(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as ManageGroupsViewModel;
            if (model != null)
            {
                model.RequestSave -= model_RequestSave;
                model.RequestRollback -= model_RequestRollback;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Groups.Items)
            {
                ContentPresenter cp = Groups.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                TextBox txtBox = cp.FindVisualChild<TextBox>();
                if (txtBox != null)
                {
                    txtBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }

    }
}
