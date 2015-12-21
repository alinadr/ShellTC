namespace ShellTC.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using ShellTC.Core;
    using ShellTC.ViewModel;

    /// <summary>
    /// Interaction logic for RightView.xaml
    /// </summary>
    public partial class RightView : UserControl
    {
        public RightView()
        {
            InitializeComponent();
            this.DataContext = new BrowserViewModel();
            FolderShell.ResponseToTheException += ResponceToTheException;
        }

        private void ResponceToTheException(object sender, EventArgs e)
        {
            try
            {
                MessageWindow mw = new MessageWindow();
                string message = "Not available";
                MessageWindowViewModel.Instance.Message = message;
                mw.ShowDialog();
            }
            catch (Exception)
            {
            }
        }
    }
}
