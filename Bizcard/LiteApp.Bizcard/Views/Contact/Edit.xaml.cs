using System.Windows.Controls;
using System.Windows.Input;
using LiteApp.Bizcard.Views.Helpers;

namespace LiteApp.Bizcard.Views.Contact
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : UserControl
    {
        public Edit()
        {
            InitializeComponent();
        }

        private void CommandBinding_Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.SaveButton.AutomationPeerInvoke();
        }
    }
}
