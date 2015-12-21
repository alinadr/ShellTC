namespace ShellTC.Core.ProccesManagers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class for implementation of notepad process
    /// </summary>
    public class NotepadProcessManager : AppProcessManager
    {
        /// <summary>
        /// Open notepad file
        /// </summary>
        /// <param name="file">path to open file from</param>
        /// <returns></returns>
        public override bool Open(string file)
        {
            try
            {
                Process.Start(file);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Create process
        /// </summary>
        /// <returns>process name</returns>
        public override string CreateStorage()
        {
            return "Notepad.exe";
        }

        /// <summary>
        /// Create file of notepad process
        /// </summary>
        /// <param name="name">name of new file</param>
        public override void CreateFile(string name)
        {
            try
            {
                string nameFile = name + DateTime.Now.ToUniversalTime().ToString().Replace(':', '_').Replace('.', '_') + ".txt";
                using (FileStream fs = File.Create(nameFile))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("");
                    fs.Write(info, 0, info.Length);
                }
                Process.Start(CreateStorage(), nameFile);
            }
            catch (Exception)
            {
            }
        }
    }
}
