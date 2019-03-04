using MessageCreator;
using Xamarin.Forms;

namespace IntroAndroidApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			EnterButton.Clicked += async (sender, args) =>
			{
				await DisplayAlert("Greetings", UserGreeting.GreetUser(TextEntry.Text), "OK");
			};
		}
	}
}