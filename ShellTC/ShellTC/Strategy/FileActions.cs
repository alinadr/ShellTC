using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellTC.Strategy
{
    public class FileActions: IStrategy
    {
        //path and name getrennt
        public void Copy(string sourcePath, string destPath, string name)
        {
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, name);
            string destFile = System.IO.Path.Combine(destPath, name);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(destFile))
            {
                System.IO.Directory.CreateDirectory(destFile);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);
        }

        //path and name zusammen
        public void Cut(string sourcePath, string destPath)
        {
            // To move a file or folder to a new location:
            System.IO.File.Move(sourcePath, destPath);
        }

        //path and name zusammen
        public void Delete(string path)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            try
            {
                fi.Delete();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Search(string path)
        {
            throw new NotImplementedException();
        }
    }
}
