using System;
using System.Threading.Tasks;
using FireAuth;
using FireAuth.iOS;
using Xamarin.Forms;
using Firebase.Auth;

[assembly: Dependency(typeof(SignupIOS))]
namespace FireAuth.iOS
{
    public class SignupIOS : ISignup
    {
        public async Task<string> SignUpWithEmailPassword(string email, string password)
        {
            if ((email.Length != 0) && (password.Length != 0))
            {
                try
                {
                    var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
                    return await user.User.GetIdTokenAsync();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}