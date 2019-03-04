using System.Windows.Forms;

namespace IntroWinFormsApp.First
{
	public partial class IntroForm : Form
	{
		public IntroForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(SayHelloToUser(textBox1.Text));
		}

		private void textBox1_GotFocused(object sender, System.EventArgs e)
		{
			AcceptButton = button1;
		}

		private string SayHelloToUser(string name) => $"Hello, {name}!";
	}
}
