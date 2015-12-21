namespace ShellTC.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    /// <summary>
    /// Base class for all shell objects
    /// </summary>
    public class ShellObject
    {
        private string _path;
        private string _size;

        /// <summary>
        /// Ctor for ShellObject
        /// </summary>
        /// <param name="path">object path</param>
        public ShellObject(string path)
        {
             Path = path;
             Size = "";
        }

        /// <summary>
        /// Gets object name
        /// </summary>
        public string Name
        {
            get
            {
                return this.Path.Substring(Path.LastIndexOf('\\') + 1);
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        /// <summary>
        /// Gets object size
        /// </summary>
        public string Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public string Icon { get; set; }

        /// <summary>
        /// Checks if object is read only
        /// </summary>
        /// <param name="attributes">object default attributes</param>
        /// <returns>boolean result</returns>
        public static bool CheckReadOnly(FileAttributes attributes)
        {
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly )
            {
                return true;
            }

            return false;
        }
    }
}
