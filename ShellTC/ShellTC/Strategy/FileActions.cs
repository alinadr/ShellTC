using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellTC.Strategy
{
    public class FileActions: IStrategy
    {
        public void Copy(string sourcePath, string destPath, string name)
        {
            
            if (!System.IO.Directory.Exists(destPath))
            {
                System.IO.Directory.CreateDirectory(destPath);
            }

            string destFile = System.IO.Path.Combine(destPath, name);

            while (System.IO.File.Exists(destFile))
            {
                name = name.Substring(0, name.LastIndexOf('.')) + "_copy" + name.Substring(name.LastIndexOf('.'));
                destFile = System.IO.Path.Combine(destPath, name);
            }

            System.IO.File.Copy(sourcePath, destFile, true);
                 
        }

        public void Cut(string sourcePath, string destPath)
        {
            System.IO.File.Move(sourcePath, destPath);
        }

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
