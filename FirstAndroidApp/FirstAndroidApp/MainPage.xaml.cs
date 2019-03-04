using Xamarin.Forms;

namespace FirstAndroidApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			EnterButton.Clicked += async (sender, args) =>
			{
				await DisplayAlert("Greetings", SayHelloToUser(TextEntry.Text), "OK");
			};
		}

		private string SayHelloToUser(string name) => $"Hello, {name}!";
	}
}