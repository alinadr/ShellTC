namespace ShellTC.Core.FileOperations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Implementation of cut paste method
    /// </summary>
    public class CutPasteMethod: PasteMethod
    {
        /// <summary>
        /// Override delete data method for cut paste implementation
        /// </summary>
        /// <param name="sourcePath">path to delete from</param>
        public override void DeleteData(string sourcePath)
        {
            FileAttributes attr = File.GetAttributes(sourcePath);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                actionStrategy = GetActions(ActionObjects.FolderShell);
                actionStrategy.Delete(sourcePath);
            }
            else
            {
                actionStrategy = GetActions(ActionObjects.FileShell);
                actionStrategy.Delete(sourcePath);
            }
        }
    }
}
