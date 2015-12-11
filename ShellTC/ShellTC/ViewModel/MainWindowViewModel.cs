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
        private string _buffer;

        public MainWindowViewModel()
        {
            leftVM = new BrowserViewModel();
            this.ViewModels.Add(leftVM);
            leftVM.PropertyChanged += this.OnLeftBufferPropertyChanged;

            rightVM = new BrowserViewModel();
            this.ViewModels.Add(rightVM);
            rightVM.PropertyChanged += this.OnRightBufferPropertyChanged;
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

        public string Buffer
        {
            get
            {
                return _buffer;
            }
            set
            {
                _buffer = value;

                
            }
        }

        private void OnLeftBufferPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Buffer")
            {
                rightVM.Buffer = leftVM.Buffer;
            }
        }

        private void OnRightBufferPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Buffer")
            {
                leftVM.Buffer = rightVM.Buffer;
            }
        }
    }
}
