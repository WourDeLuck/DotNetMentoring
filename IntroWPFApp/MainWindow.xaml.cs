using MessageCreator;
using System;
using System.Windows;

namespace IntroWPFApp
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
			MessageBox.Show(UserGreeting.GreetUser(EntryPoint.Text));
		}
	}
}