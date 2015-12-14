using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellTC.Strategy
{
    public class FolderActions: IStrategy
    {
        public void Copy(string sourcePath, string destPath, string name = null)
        {
            DirectoryInfo source = new DirectoryInfo(sourcePath);
            DirectoryInfo destination = new DirectoryInfo(destPath);

            name = source.Name;

            while (System.IO.Directory.Exists(destPath + "\\" + name))
            {
                name = name + "_copy";
            }
            DirectoryInfo dest = destination.CreateSubdirectory(name);
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourcePath, dest.FullName, true);
        }

        public void Cut(string sourcePath, string destPath)
        {
            System.IO.Directory.Move(sourcePath, destPath);
        }

        public void Delete(string path)
        {
            DirectoryInfo di = new System.IO.DirectoryInfo(path);
            try
            {
                di.Delete(true);
            }
            catch (System.IO.IOException e)
            {
            }
        }

        public void Search(string path)
        {
            throw new NotImplementedException();
        }
    }
}
