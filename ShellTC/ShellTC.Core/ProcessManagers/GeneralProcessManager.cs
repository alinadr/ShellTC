namespace ShellTC.Core.ProccesManagers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class implementation of AppProcessManager
    /// </summary>
    public class GeneralProcessManager : AppProcessManager
    {
        public override string CreateStorage()
        {
            throw new NotImplementedException();
        }

        public override void CreateFile(string name)
        {
            throw new NotImplementedException();
        }

        public override bool Open(string file)
        {
            try
            {
                Process.Start(file);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
