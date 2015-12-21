namespace ShellTC.Core.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for strategy pattern, file and folder actions
    /// </summary>
    public interface IStrategy
    {
        void Copy(string sourcePath, string destPath, string name);

        void Cut(string sourcePath, string destPath);
        
        void Delete(string path);
        
        void Search(string path);
    }
}
