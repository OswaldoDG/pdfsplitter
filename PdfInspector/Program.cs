using PdfInspector.Domain.Models.Auth;
using PdfInspector.Forms;
using System;
using System.Windows.Forms;

namespace PdfInspector
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            var container = DI.Configure();

            while (true)
            {
                using (var loginForm = container.GetInstance<LoginForm>())
                {
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                }
                using (var appForm = container.GetInstance<Form1>())
                {
                    System.Windows.Forms.Application.Run(appForm);

                    if (appForm.IsLoggingOut)
                    {
                        container.GetInstance<UsuarioSesion>().Clear();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
