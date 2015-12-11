namespace ShellTC.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MessageWindowViewModel : ViewModelBase
    {
        private static readonly object _objLock = new object();
        private static MessageWindowViewModel _instance = null;
        private string _message;

        private MessageWindowViewModel()
        {
        }

        public static MessageWindowViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MessageWindowViewModel();
                        }
                    }
                }

                return _instance;                
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }
    }
}
