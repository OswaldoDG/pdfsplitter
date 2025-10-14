using PdfInspector.Forms;
using System;
using System.Windows.Forms;

namespace PdfInspector
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var container = DI.Configure();
            var loginForm = container.GetInstance<LoginForm>();
            Application.Run(loginForm);
        }
    }
}
