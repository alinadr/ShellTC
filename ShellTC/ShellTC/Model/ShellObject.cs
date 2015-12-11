using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellTC.Model
{
    public class ShellObject
    {
        private string _name;
        private string _path;
        private string _size;
        private string _icon;

        public ShellObject(string path)
        {
             Path = path;
        }

        public string Name
        {
            get
            {
                return this.Path.Substring(Path.LastIndexOf('\\') + 1);
            }
        }

        public string Path { get; set; }
        public long Size { get; set; }
        public string Icon { get; set; }

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
