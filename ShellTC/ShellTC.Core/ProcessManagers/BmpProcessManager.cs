namespace ShellTC.Core.ProccesManagers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class for implementation of bmp process
    /// </summary>
    public class BmpProcessManager : AppProcessManager
    {
        /// <summary>
        /// Open bmp file
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
            return "mspaint.exe";
        }

        /// <summary>
        /// Create file of bmp process
        /// </summary>
        /// <param name="name">name of new file</param>
        public override void CreateFile(string name)
        {
            try
            {
                string nameFile = name + DateTime.Now.ToUniversalTime().ToString().Replace(':', '_').Replace('.', '_') + ".png";
                using (FileStream fs = File.Create(nameFile))
                {
                }
                var paint = new Process();
                paint.StartInfo.FileName = @"C:\Windows\System32\mspaint.exe";
                paint.StartInfo.Arguments = string.Format("\"{0}\"", nameFile);
                paint.Start();

            }
            catch (Exception)
            {
            }
        }
    }
}
