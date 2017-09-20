using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sxnqcc
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm login = new LoginForm();

            if (login.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm());
            }
        }
    }
}
