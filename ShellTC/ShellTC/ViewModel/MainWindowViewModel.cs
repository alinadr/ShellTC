namespace ShellTC.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for MainWindow view model
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _viewModels;
        private BrowserViewModel leftVM;
        private BrowserViewModel rightVM;

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class
        /// </summary>
        public MainWindowViewModel()
        {
            leftVM = new BrowserViewModel();
            this.ViewModels.Add(leftVM);

            rightVM = new BrowserViewModel();
            this.ViewModels.Add(rightVM);
        }

        /// <summary>
        /// Gets collection of view models
        /// </summary>
        public ObservableCollection<ViewModelBase> ViewModels
        {
            get
            {
                if (_viewModels == null)
                {
                    _viewModels = new ObservableCollection<ViewModelBase>();
                }

                return _viewModels;
            }
        }
    }
}
