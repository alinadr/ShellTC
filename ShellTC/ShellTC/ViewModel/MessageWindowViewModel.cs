namespace ShellTC.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for MessageWindow view model
    /// </summary>
    public class MessageWindowViewModel : ViewModelBase
    {
        private static readonly object _objLock = new object();
        private static MessageWindowViewModel _instance = null;
        private string _message;

        /// <summary>
        /// Prevents a default instance of the MessageWindowViewModel class from being created
        /// </summary>
        private MessageWindowViewModel()
        {
        }

        /// <summary>
        /// Gets instance of MessageWindowViewModel
        /// </summary>
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

        /// <summary>
        /// Gets or sets message to print out
        /// </summary>
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
