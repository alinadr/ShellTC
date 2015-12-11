namespace ShellTC.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ShellTC.View;
    using ShellTC.ViewModel;
    using System.Collections.ObjectModel;

    public class FolderShell:ShellObject
    {
        private bool _isDrive;
        private ReadOnlyCollection<ShellObject> _files;
        private ReadOnlyCollection<ShellObject> _folders;
        private bool _isEnabled;

        public FolderShell(string path, bool isDrive = false, bool isEnabled = true): base(path)
        {
            IsDrive = isDrive;
            IsEnabled = isEnabled;
        }

        public new string Icon
        {
            get
            {
                return "\\Images\\folder.png";
            }
        }

        public bool IsDrive { get; set; }

        public ReadOnlyCollection<ShellObject> Files
        {
            get
            {
                try
                {
                    List<ShellObject> files = new List<ShellObject>();
                    string[] allFiles = Directory.GetFiles(Path);

                    foreach (var file in allFiles)
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

                        if (!CheckReadOnly(fileInfo.Attributes))
                        {
                            files.Add(new FileShell(file));
                        }
                    }
                    _files = new ReadOnlyCollection<ShellObject>(files);
                }
                catch (Exception e)
                {
                    IsEnabled = false;
                    //ResponceToTheException(e);
                }

                return _files;
            }
        }

        public ReadOnlyCollection<ShellObject> Folders
        {
            get
            {
                try
                {
                    List<ShellObject> folders = new List<ShellObject>();
                    string[] allFolders = Directory.GetDirectories(Path);
                    foreach (var folder in allFolders)
                    {
                        System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(folder);
                        if (!CheckReadOnly(dirInfo.Attributes))
                        {
                            folders.Add(new FolderShell(folder));
                        }
                    }
                    _folders = new ReadOnlyCollection<ShellObject>(folders);
                }
                catch (Exception e)
                {
                    IsEnabled = false;
                    ResponceToTheException(e);
                }
                
                return _folders;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                _isEnabled = value;
            }
        }

        public long countSize()
        {
            long size = 0;
            string[] allFiles = Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories);
            foreach (var file in allFiles)
            {
                size += file.Length;
            }

            return size;
        }

        private void GetDirectorySize(string directory)
        {
            foreach (string dir in Directory.GetDirectories(directory))
            {
                GetDirectorySize(dir);
            }

            foreach (FileInfo file in new DirectoryInfo(directory).GetFiles())
            {
                this.Size += file.Length;
            }
        }

        public static void ResponceToTheException(Exception except)
        {
            MessageWindow mw = new MessageWindow();
            MessageWindowViewModel.Instance.Message = except.Message;
            mw.ShowDialog();
        }


    }
}
