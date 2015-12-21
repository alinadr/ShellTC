namespace ShellTC.Core.ProccesManagers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Abstract class for factory method of process manager
    /// </summary>
    public abstract class AppProcessManager
    {
        public abstract string CreateStorage();

        public abstract bool Open(string file);

        public abstract void CreateFile(string name);
    }
}
