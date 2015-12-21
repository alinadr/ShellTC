namespace ShellTC.Core.FileOperations
{
    using ShellTC.Core.Actions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Abstract class for template method
    /// </summary>
    public abstract class PasteMethod
    {
        public IStrategy actionStrategy = null;

        /// <summary>
        /// Copy data
        /// </summary>
        /// <param name="sourcePath">path where to copy from</param>
        /// <param name="destPath">path where to copy</param>
        public void CopyData(string sourcePath, string destPath)
        {
            FileAttributes attr = File.GetAttributes(sourcePath);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                actionStrategy = GetActions(ActionObjects.FolderShell);
                actionStrategy.Copy(sourcePath, destPath, null);
            }
            else
            {
                string name = sourcePath.Substring(sourcePath.LastIndexOf('\\') + 1);
                actionStrategy = GetActions(ActionObjects.FileShell);
                actionStrategy.Copy(sourcePath, destPath, name);
            }
            
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="sourcePath">path delete from</param>
        public abstract void DeleteData(string sourcePath);

        /// <summary>
        /// Paste data
        /// </summary>
        /// <param name="sourcePath">path to paste what</param>
        /// <param name="destPath">path to paste to</param>
        public void Paste(string sourcePath, string destPath)
        {
            this.CopyData(sourcePath, destPath);
            string newPath = destPath + "\\" + sourcePath.Substring(sourcePath.LastIndexOf("\\") + 1);
            if (sourcePath.Equals(newPath))
            {
                FileAttributes attr = File.GetAttributes(sourcePath);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    this.DeleteData(sourcePath + "_copy");
                }
                else
                {
                    newPath = sourcePath.Substring(0, sourcePath.LastIndexOf('.')) + "_copy" + sourcePath.Substring(sourcePath.LastIndexOf('.'));
                    this.DeleteData(newPath);
                }
            }
            else
            {
                this.DeleteData(sourcePath);
            }
        }
        
        /// <summary>
        /// Get actions depending on action obect type
        /// </summary>
        /// <param name="aObj">action object type</param>
        /// <returns>IStrategy object</returns>
        public static IStrategy GetActions(ActionObjects aObj)
        {
            IStrategy actions = null;

            switch (aObj)
            {
                case ActionObjects.FileShell:
                    actions = new FileActions();
                    break;
                case ActionObjects.FolderShell:
                    actions = new FolderActions();
                    break;
                default:
                    break;
            }

            return actions;
        }
    }
}
