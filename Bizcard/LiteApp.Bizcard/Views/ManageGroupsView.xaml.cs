using System.Windows;
using System.Windows.Controls;
using LiteApp.Bizcard.Helpers;
using LiteApp.Bizcard.Resources;
using LiteApp.Bizcard.ViewModels;
using LiteApp.Bizcard.Views.Helpers;

namespace LiteApp.Bizcard.Views
{
    /// <summary>
    /// Interaction logic for ManageGroupView.xaml
    /// </summary>
    public partial class ManageGroupsView : MetroWindowEx
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
            (this.DataContext as ManageGroupsViewModel).ValidationErrors = validationError;
            if (validationError.Length == 0)
            {
                this.DialogResult = true;
            }
        }

        private void Rollback_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ManageGroupsView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Save.AutomationPeerInvoke();
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                Rollback.AutomationPeerInvoke();
            }
        }
    }
}
