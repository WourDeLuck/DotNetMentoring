using System;
using System.Windows;

namespace IntroWPFApp.First
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public void Button_Click(object sender, EventArgs e)
		{
			MessageBox.Show(SayHelloToUser(EntryPoint.Text));
		}

		private string SayHelloToUser(string name) => $"Hello, {name}!";
	}
}
