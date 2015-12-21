namespace ShellTC.Core.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for implementation of file actions
    /// </summary>
    public class FileActions: IStrategy
    {
        /// <summary>
        /// Copy file
        /// </summary>
        /// <param name="sourcePath">path to copy from</param>
        /// <param name="destPath">path to copy to</param>
        /// <param name="name">file name</param>
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

        /// <summary>
        /// Cut file
        /// </summary>
        /// <param name="sourcePath">path to cut from</param>
        /// <param name="destPath">path to move to</param>
        public void Cut(string sourcePath, string destPath)
        {
            System.IO.File.Move(sourcePath, destPath);
        }

        /// <summary>
        /// Delete file
        /// </summary>
        /// <param name="path">path of file to delete</param>
        public void Delete(string path)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            try
            {
                fi.Delete();
            }
            catch
            {
            }
        }

        public void Search(string path)
        {
            throw new NotImplementedException();
        }
    }
}
