using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		private string SayHelloToUser(string name) => $"Hello, {name}!";
	}
}