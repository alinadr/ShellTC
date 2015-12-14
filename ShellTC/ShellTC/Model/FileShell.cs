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
            FileInfo info = new FileInfo(Path);
            long size;
            if (!ShellObject.CheckReadOnly(info.Attributes))
            {
                size = info.Length;
            }
            else
            {
                size = 0;
            }
            size = size / 1024;
            base.Size = size.ToString() + "KB";
        }

        public new string Icon
        {
            get
            {
                return "\\Images\\file.png";
            }
        }

        //public long countSize()
        //{
        //    FileInfo info = new FileInfo(Path);
        //    return info.Length;
        //}
    }
}
