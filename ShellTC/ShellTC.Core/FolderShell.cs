namespace ShellTC.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Class that represents folder object
    /// </summary>
    public class FolderShell:ShellObject
    {
        private ReadOnlyCollection<ShellObject> _files;
        private ReadOnlyCollection<ShellObject> _folders;
        private List<ShellObject> shellObjects = new List<ShellObject>();

        public static EventHandler ResponseToTheException;

        /// <summary>
        /// Ctor for FolderShell
        /// </summary>
        /// <param name="path">folder path</param>
        /// <param name="isDrive">boolean, if folder is a drive</param>
        public FolderShell(string path, bool isDrive = false): base(path)
        {
            IsDrive = isDrive;
        }

        public new string Icon
        {
            get
            {
                return "\\Images\\folder.png";
            }
        }

        public bool IsDrive { get; set; }

        /// <summary>
        /// Gets all files in folder
        /// </summary>
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
                catch
                {
                }

                return _files;
            }
        }

        /// <summary>
        /// Gets all folders in this folder
        /// </summary>
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
                    OnResponseToTheException(e);
                }
                
                return _folders;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="except"></param>
        private static void OnResponseToTheException(Exception except)
        {
            if (ResponseToTheException != null)
            {
                ResponseToTheException(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Recursive search in current folder
        /// </summary>
        /// <param name="path">path where to search</param>
        /// <param name="pattern">string to search for</param>
        /// <param name="isRecursive">bool recurtion</param>
        /// <returns>list of objects that </returns>
        public List<ShellObject> RecursiveSearch(string path, string pattern, bool isRecursive = false)
        {
            if (!isRecursive)
                shellObjects.Clear();
            try
            {
                foreach (string d in Directory.GetDirectories(path))
                {
                    if (new FolderShell(d).Name.ToLower().Contains(pattern.ToLower()))
                        shellObjects.Add(new FolderShell(d));
                    if (!ShellObject.CheckReadOnly(new DirectoryInfo(d).Attributes))
                    {
                        foreach (string f in Directory.GetFiles(d))
                        {
                            if (new FileShell(f).Name.ToLower().Contains(pattern.ToLower()))
                                shellObjects.Add(new FileShell(f));
                        }
                        RecursiveSearch(d, pattern, true);
                    }
                }
                return shellObjects;
            }
            catch (Exception ex)
            {
                OnResponseToTheException(ex);
                return null;
            }
        }
    }
}
