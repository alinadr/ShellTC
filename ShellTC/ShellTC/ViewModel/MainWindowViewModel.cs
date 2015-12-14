namespace ShellTC.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _viewModels;
        private BrowserViewModel leftVM;
        private BrowserViewModel rightVM;

        public MainWindowViewModel()
        {
            leftVM = new BrowserViewModel();
            this.ViewModels.Add(leftVM);

            rightVM = new BrowserViewModel();
            this.ViewModels.Add(rightVM);
        }

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
