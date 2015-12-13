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
        private bool flag = true;

        public void Copy(string sourcePath, string destPath, string name = null)
        {
            DirectoryInfo source = new DirectoryInfo(sourcePath);
            DirectoryInfo destination = new DirectoryInfo(destPath);

            if (destination.FullName.Contains(source.FullName))
                throw new Exception("Cannot perform DeepCopy: Ancestry conflict detected");
            if (flag)
            {
                DirectoryInfo dest = destination.CreateSubdirectory(source.Name);
                flag = false;
                this.Copy(source.FullName, dest.FullName);
                return;
            }
            

            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                DirectoryInfo newDest = destination.CreateSubdirectory(dir.Name);
                this.Copy(dir.FullName, newDest.FullName);
            }
            foreach (FileInfo file in source.GetFiles())
                new FileActions().Copy(file.FullName, destination.FullName, file.Name);
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
