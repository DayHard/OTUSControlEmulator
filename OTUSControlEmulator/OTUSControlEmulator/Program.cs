using System;
using System.Windows.Forms;

namespace OTUSControlEmulator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogo());
            Application.Run(new FrmMain());
        }

        static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex == null) return;
           
            MessageBox.Show(ex.Message,@"Global exeption",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
