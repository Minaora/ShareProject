using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SharePoint
{
	public partial class MainPage : ContentPage
	{
        SharePoint.MainPage context;
		public MainPage()
		{
			InitializeComponent();
            Wrapper app = Wrapper.Instance;
            registerButton.Clicked += delegate
            {
                App.Current.MainPage = new Page1();
            };
            loginButton.Clicked += delegate {
                Task.Run(async () =>
                {
                    bool json = await app.TryToLogin(usernameEntry.Text, passwordEntry.Text);
                    if (json == true)
                    {
                        Console.WriteLine("Podarilo sa prihlásiť");
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            var answer = DisplayAlert("Prihlásenie", "Úspešne prihlásený", "OK");
                        });
                    }
                    else
                    {
                        Console.WriteLine("Nepodarilo sa prihlásiť");
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            var answer = DisplayAlert("Prihlásenie", "Nepodarilo sa prihlásiť", "OK");
                        });
                    }
                });
            };
        }
	}
}
