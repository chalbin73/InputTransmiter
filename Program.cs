using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Albin.Commons.Logging;

namespace InputTransmiter
{
    public static class Program
    {
        public static Version version = Version.Parse("0.0.0.1");
        

        public static Log log = new Log();
        public static string Language;
        
        /// <summary>
        ///
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log.log("Starting program", LogType.INFO);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
