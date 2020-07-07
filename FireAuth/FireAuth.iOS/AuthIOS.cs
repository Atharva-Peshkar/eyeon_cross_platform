using System;
using System.Threading.Tasks;
using FireAuth;
using FireAuth.iOS;
using Xamarin.Forms;
using Firebase.Auth;
using System.Linq.Expressions;

[assembly: Dependency(typeof(AuthIOS))]
namespace FireAuth.iOS
{
    public class AuthIOS : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            if ((email.Length != 0) && (password.Length != 0))
            {
                try
                {
                    var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                    return await user.User.GetIdTokenAsync();
                }
                catch (Exception e)
                {
                    return "";
                }

            }
            else
            {
                try
                {
                    return "";
                }
                catch (Exception empty) { return ""; }
 
            }
        }
    }
}