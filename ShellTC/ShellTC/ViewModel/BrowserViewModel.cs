namespace ShellTC.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ShellTC.Core;
    using ShellTC.Core.Actions;
    using ShellTC.Core.FileOperations;
    using ShellTC.Core.ProccesManagers;
    using ShellTC.View;

    public class BrowserViewModel : ViewModelBase
    {
        private static string[] _buffer = new string[2];

        private static string[] _drives = System.IO.Directory.GetLogicalDrives();
        private IStrategy actionStrategy = null;

        private string _path;
        private string _searchPath;
        private string _icon;
        private ShellObject _selected = null;
        private FolderShell _drive;
        private ReadOnlyObservableCollection<ShellObject> _allDrives;
        private ReadOnlyObservableCollection<ShellObject> _allObjects;

        public BrowserViewModel()
        {
            Drive = (FolderShell)AllDrives[0];
            this.GoToPathCommand = new Command(arg => GoToPath(this.Path));
            this.OpenItemCommand = new Command(arg => OpenItem());
            this.GoUpCommand = new Command(arg => GoUp(this.Path));
            this.PasteCommand = new Command(arg => Paste(this.Path));
            this.CopyCommand = new Command(arg => Copy(this.Selected));
            this.CutCommand = new Command(arg => Cut(this.Selected));
            this.DeleteCommand = new Command(arg => Delete(this.Selected));
            this.SearchCommand = new Command(Search);
            this.CreateTxtFileCommand = new Command(args => CreateTxtFile());
            this.CreateBmpFileCommand = new Command(args => CreatePngFile());
        }

        #region Commands

        public Command GoToPathCommand { get; set; }

        public Command OpenItemCommand { get; set; }

        public Command GoUpCommand { get; set; }
        
        public Command CopyCommand { get; set; }
        
        public Command PasteCommand { get; set; }
        
        public Command CutCommand { get; set; }
        
        public Command DeleteCommand { get; set; }
        
        public Command SearchCommand { get; set; }
        
        public Command CreateTxtFileCommand { get; set; }
        
        public Command CreateBmpFileCommand { get; set; }

        #endregion

        #region Properties

        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        public string SearchPath
        {
            get
            {
                return _searchPath;
            }

            set
            {
                _searchPath = value;
                OnPropertyChanged("SearchPath");
            }
        }

        public string Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                OnPropertyChanged("Icon");
            }
        }

        public string[] Buffer
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

        public ShellObject Selected
        {
            get
            {
                return _selected;
            }

            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        public FolderShell Drive
        {
            get
            {
                return _drive;
            }

            set
            {
                try
                {
                    if (value != null)
                    {
                        _drive = value;
                        Path = Drive.Path;
                        List<ShellObject> obj = value.Folders.Concat(value.Files).ToList();
                        _allObjects = new ReadOnlyObservableCollection<ShellObject>(new ObservableCollection<ShellObject>(obj));
                        OnPropertyChanged("Drive");
                        OnPropertyChanged("AllObjects");
                    }
                }
                catch(Exception)
                {
                    _allObjects = null;
                    OnPropertyChanged("Drive");
                    OnPropertyChanged("AllObjects");
                }
            }
        }

        public ReadOnlyObservableCollection<ShellObject> AllDrives
        {
            get
            {
                if (_allDrives == null)
                {
                    ObservableCollection<ShellObject> allDrives = new ObservableCollection<ShellObject>();
                    foreach (var drive in _drives)
                    {
                        allDrives.Add(new FolderShell(drive, true));
                    }

                    _allDrives = new ReadOnlyObservableCollection<ShellObject>(allDrives);
                }

                return _allDrives;
            }
        }
       
        public ReadOnlyObservableCollection<ShellObject> AllObjects
        {
            get
            {
                return _allObjects;
            }

            private set
            {
                _allObjects = value;
                OnPropertyChanged("AllObjects");
            }
        }

        #endregion

        #region Command Actions

        public void GoToPath(string destPath)
        {
            try
            {
                if (!destPath.Substring(0, 3).Equals(Drive.Path) && !string.IsNullOrEmpty(destPath))
                {
                    int index = AllDrives.IndexOf(AllDrives.Where(item => item.Path.Equals(destPath.Substring(0, 3))).First());
                    Drive = (FolderShell)AllDrives[index];
                }

                FileAttributes attr = File.GetAttributes(destPath);
                
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Path = destPath;
                }
                else
                {
                    Path = destPath.Substring(0, destPath.LastIndexOf('\\'));
                }

                FolderShell f = new FolderShell(Path);
                List<ShellObject> obj = f.Folders.Concat(f.Files).ToList();
                AllObjects = new ReadOnlyObservableCollection<ShellObject>(new ObservableCollection<ShellObject>(obj));
            }
            catch (Exception e)
            {
                Path = Drive.Path;
                MessageWindow mw = new MessageWindow();
                MessageWindowViewModel.Instance.Message = e.Message;
                mw.ShowDialog();
            }
        }

        public void OpenItem()
        {
            FileAttributes attr = File.GetAttributes(Selected.Path);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                GoToPath(Selected.Path);
            }
            else
            {
                GeneralProcessManager manager = new GeneralProcessManager();

                manager.Open(Selected.Path);
            }
        }

        public void GoUp(string pathInit)
        {
            if (string.IsNullOrEmpty(pathInit))
            {
                if (pathInit.Length > 3)
                {
                    if (!(pathInit.LastIndexOf('\\') == 2))
                    {
                        this.Path = pathInit.Substring(0, pathInit.LastIndexOf('\\'));
                    }
                    else
                    {
                        this.Path = pathInit.Substring(0, pathInit.LastIndexOf('\\') + 1);
                    }
                }
                else
                {
                    Path = Drive.Path;
                }

                GoToPath(Path);
            }
        }
       
        public void Paste(string destPath)
        {
            try
            {
                PasteMethod method = null;

                if (Buffer[1].Equals("copy"))
                {
                    method = new CopyPasteMethod();
                    method.Paste(Buffer[0], destPath);
                }
                
                if (Buffer[1].Equals("cut"))
                {
                    method = new CutPasteMethod();
                    method.Paste(Buffer[0], destPath);
                    Buffer[1] = "copy";
                }

                GoToPath(destPath);
            }
            catch (NullReferenceException e)
            {
                string message = "Nothing to paste";
                ResponceToTheException(e, message);
            }
        }

        public void Copy(ShellObject source)
        {
            try
            {
                if (source != null)
                {
                    string sourcePath = source.Path;
                    Buffer[0] = sourcePath;
                    Buffer[1] = "copy";
                }
            }
            catch (NullReferenceException e)
            {
                string message = "Select an item to copy";
                ResponceToTheException(e, message);
            }
        }

        public void Cut(ShellObject source)
        {
            try
            {
                if (source != null)
                {
                    string sourcePath = source.Path;
                    Buffer[0] = sourcePath;
                    Buffer[1] = "cut";
                }
            }
            catch (NullReferenceException e)
            {
                string message = "Select an item to cut";
                ResponceToTheException(e, message);
            }
        }

        public void Delete(ShellObject source)
        {
            try
            {
                string sourcePath = source.Path;

                FileAttributes attr = File.GetAttributes(sourcePath);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    actionStrategy = GetActions(ActionObjects.FolderShell);
                    actionStrategy.Delete(sourcePath);
                }
                else
                {
                    actionStrategy = GetActions(ActionObjects.FileShell);
                    actionStrategy.Delete(sourcePath);
                }

                GoToPath(sourcePath.Substring(0, sourcePath.LastIndexOf('\\')));
            }
            catch (NullReferenceException e)
            {
                string message = "Select an item to delete";
                ResponceToTheException(e, message);
            }
        }

        public void Search(object parameter)
        {
            try
            {
                if (parameter != null)
                {
                    List<ShellObject> searchRes = Drive.RecursiveSearch(Path, parameter.ToString());
                    AllObjects = new ReadOnlyObservableCollection<ShellObject>(new ObservableCollection<ShellObject>(searchRes));
                }
            }
            catch (Exception e)
            {
            }
        }

        public void CreateTxtFile()
        {
            NotepadProcessManager manager = new NotepadProcessManager();
            manager.CreateFile(Path + "\\new file");
            OnPropertyChanged("AllObjects");
        }

        public void CreatePngFile()
        {
            BmpProcessManager manager = new BmpProcessManager();
            manager.CreateFile(Path + "\\new file");
            OnPropertyChanged("AllObjects");
        }
        #endregion

        #region Helpers

        public static void ResponceToTheException(Exception except, string message = null)
        {
            if (except != null)
            {
                MessageWindow mw = new MessageWindow();
                if (string.IsNullOrEmpty(message))
                {
                    MessageWindowViewModel.Instance.Message = except.Message;
                }
                else
                {
                    MessageWindowViewModel.Instance.Message = message;
                }

                mw.ShowDialog();
            }
        }

        ////public void CancelReadOnlyAttributes(ShellObject drive)
        ////{
        ////    DirectoryInfo di = new DirectoryInfo(drive.Path);
        ////    RemoveReadOnlyFlag(di);
        ////}

        ////public void RemoveReadOnlyFlag(DirectoryInfo di)
        ////{
        ////    if (ShellObject.CheckReadOnly(di.Attributes))
        ////    {
        ////        di.Attributes = FileAttributes.Normal;
        ////        ReadOnlyPaths.Add(di.FullName);
        ////    }
           
        ////    foreach (DirectoryInfo dif in di.GetDirectories())
        ////    {
        ////        try
        ////        {
        ////            RemoveReadOnlyFlag(dif);
        ////        }
        ////        catch 
        ////        {                    
        ////        }
        ////    }

        ////    foreach (FileInfo fi in di.GetFiles())
        ////    {
        ////        if (ShellObject.CheckReadOnly(fi.Attributes))
        ////        {
        ////            fi.Attributes = FileAttributes.Normal;
        ////            ReadOnlyPaths.Add(fi.FullName);
        ////        }
        ////    }
        ////}

        private static IStrategy GetActions(ActionObjects aObj)
        {
            IStrategy actions = null;

            switch (aObj)
            {
                case ActionObjects.FileShell:
                    actions = new FileActions();
                    break;
                case ActionObjects.FolderShell:
                    actions = new FolderActions();
                    break;
                default:
                    break;
            }

            return actions;
        }
        #endregion
    }
}
