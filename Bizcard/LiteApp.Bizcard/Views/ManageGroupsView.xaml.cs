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
using LiteApp.Bizcard.Resources;

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
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string validationError = string.Empty;
            foreach (var item in Groups.Items)
            {
                ContentPresenter cp = Groups.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                TextBox txtBox = cp.FindVisualChild<TextBox>();
                if (txtBox != null)
                {
                    if (string.IsNullOrWhiteSpace(txtBox.Text))
                    {
                        validationError = ValidationErrorStrings.GroupNameRequired;
                        break;
                    }
                    var binding = txtBox.GetBindingExpression(TextBox.TextProperty);
                    binding.UpdateSource();
                }
            }
            (this.DataContext as ManageGroupsViewModel).HasValidationErrors = validationError.Length > 0;
            if (validationError.Length == 0)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show(validationError, "Manage Groups", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Rollback_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
