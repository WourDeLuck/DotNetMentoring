using System;
using System.Windows.Forms;
using MessageCreator;

namespace IntroWinFormsApp
{
	public partial class IntroForm : Form
	{
		public IntroForm()
		{
			InitializeComponent();
		}

		private void EnterName_Click(object sender, EventArgs e)
		{
			MessageBox.Show(UserGreeting.GreetUser(NameTextBox.Text));
		}

		private void NameTextBox_GotFocused(object sender, EventArgs e)
		{
			AcceptButton = EnterName;
		}

		private string SayHelloToUser(string name) => $"Hello, {name}!";
	}
}