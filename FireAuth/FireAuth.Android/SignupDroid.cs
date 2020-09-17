using System;
using System.Threading.Tasks;
using FireAuth;
using FireAuth.Droid;
using Firebase.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignupDroid))]
namespace FireAuth.Droid
{
    public class SignupDroid : ISignup
    {
        public async Task<string> SignUpWithEmailPassword(string email, string password)
        {
            if ((email.Length != 0) && (password.Length != 0))
            {
                try
                {
                    var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                    var token = await user.User.GetIdTokenAsync(false);
                    return token.Token;
                }
                catch (FirebaseAuthUserCollisionException e)
                {
                    e.PrintStackTrace();
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