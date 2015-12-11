using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellTC.Strategy
{
    public interface IStrategy
    {
        void Copy(string sourcePath, string destPath, string name);
        void Cut(string sourcePath, string destPath);
        void Delete(string path);
        void Search(string path);
    }
}
