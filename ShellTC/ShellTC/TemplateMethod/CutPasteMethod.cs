using ShellTC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShellTC.TemplateMethod
{
    public class CutPasteMethod: PasteMethod
    {
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
