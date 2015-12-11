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

            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                DirectoryInfo newDest = destination.CreateSubdirectory(dir.Name);
                this.Copy(dir.FullName, newDest.FullName);
            }
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(destination.FullName, file.Name));
        }

        public void Cut(string sourcePath, string destPath)
        {
            System.IO.Directory.Move(sourcePath, destPath);
        }

        public void Delete(string path)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
            // Delete this dir and all subdirs.
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
