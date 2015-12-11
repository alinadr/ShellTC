namespace ShellTC.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileShell: ShellObject 
    {
        public FileShell(string path): base(path)
        {
        }

        public new string Icon
        {
            get
            {
                return "\\Images\\file.png";
            }
        }

        public new long Size
        {
            get
            {
                FileInfo info = new FileInfo(Path);

                if (ShellObject.CheckReadOnly(info.Attributes))
                {
                    return info.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        public long countSize()
        {
            FileInfo info = new FileInfo(Path);
            return info.Length;
        }
    }
}
