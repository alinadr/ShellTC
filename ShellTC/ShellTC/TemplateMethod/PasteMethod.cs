using ShellTC.Model;
using ShellTC.Strategy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShellTC.TemplateMethod
{
    public abstract class PasteMethod
    {
        public IStrategy actionStrategy = null;

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

        public abstract void DeleteData(string sourcePath);

        public void Paste(string sourcePath, string destPath)
        {
            this.CopyData(sourcePath, destPath);
            this.DeleteData(sourcePath);
        }

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
