using System;
using System.Threading.Tasks;
using FireAuth;
using FireAuth.Droid;
using Firebase.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthDroid))]
namespace FireAuth.Droid
{
    public class AuthDroid : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            if((email.Length != 0) && (password.Length != 0))
            {
                try
                {
                    var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                    var token = await user.User.GetIdTokenAsync(false);
                    return token.Token;
                }
                catch (FirebaseAuthInvalidUserException e)
                {
                    e.PrintStackTrace();
                    return "";
                }
                catch (Exception a)
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