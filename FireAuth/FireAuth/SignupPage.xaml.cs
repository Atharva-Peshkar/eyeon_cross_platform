using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FireAuth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SignupPage : ContentPage
    {
        ISignup account_creator;
        public SignupPage()
        {
            InitializeComponent();
            account_creator = DependencyService.Get<ISignup>();
        }

        /*The handler for button click - Sign Up*/
        async void CreateNewAccount(object sender, EventArgs e)
        {
            string Token = string.Empty;
            try
            {
                Token = await account_creator.SignUpWithEmailPassword(NewEmailID.Text, NewPassword.Text);
            } catch(Exception) { await DisplayAlert("Error!", "An error occurred, please try again!", "Ok"); }
            if (Token != "")
            {
                await DisplayAlert("SignUp Successful!", "Please sign into your account to proceed.", "Ok");
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                ShowErrorSignup();
            }
        }

        async private void ShowErrorSignup()
        {
            await DisplayAlert("Sign Up Failed", "Email ID already in use!", "OK");
        }

    }
}