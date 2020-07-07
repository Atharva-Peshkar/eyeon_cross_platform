using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FireAuth
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        IAuth auth;

        public MainPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async void LoginClicked(object sender, EventArgs e)
        {
            string Token=string.Empty;
            try
            {
                Token = await auth.LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text);
            } catch(Exception signin) { await DisplayAlert("Error!","An error occurred, please try again!","Ok"); }
            if (Token != "")
            {
                bool allowed = false;
                allowed = await GoogleVisionBarCodeScanner.Methods.AskForRequiredPermission();
                if (allowed)
                {
                    await Navigation.PushAsync(new LoggedPage());
                    EmailInput.Text = String.Empty;
                    PasswordInput.Text = String.Empty;
                }
                else
                {
                    await DisplayAlert("Alert", "Please provide Camera permission to proceed!", "Ok");
                }
            }
            else
            {
                ShowError();
            }
            
        }

        async private void SignUpClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new SignupPage());
        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }
    }
}