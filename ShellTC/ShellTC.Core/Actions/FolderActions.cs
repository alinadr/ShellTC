namespace ShellTC.Core.Actions
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for implementation of folder actions
    /// </summary>
    public class FolderActions: IStrategy
    {
        /// <summary>
        /// Copy folder
        /// </summary>
        /// <param name="sourcePath">path to copy from</param>
        /// <param name="destPath">path to copy to</param>
        /// <param name="name">name</param>
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

        /// <summary>
        /// Cut folder
        /// </summary>
        /// <param name="sourcePath">path to cut from</param>
        /// <param name="destPath">path to move to</param>
        public void Cut(string sourcePath, string destPath)
        {
            System.IO.Directory.Move(sourcePath, destPath);
        }

        /// <summary>
        /// Delete folder
        /// </summary>
        /// <param name="path">path of folder to delete</param>
        public void Delete(string path)
        {
            DirectoryInfo di = new System.IO.DirectoryInfo(path);
            try
            {
                di.Delete(true);
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
