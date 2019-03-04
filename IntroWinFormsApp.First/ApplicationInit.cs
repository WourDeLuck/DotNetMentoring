using System;
using System.Windows.Forms;

namespace IntroWinFormsApp.First
{
	static class ApplicationInit
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new IntroForm());
		}
	}
}
