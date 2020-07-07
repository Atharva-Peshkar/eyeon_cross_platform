using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FireAuth
{
    public class PasswordValidationBehavior : Behavior<Entry>
    {
        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = (e.NewTextValue.Length>6 && Regex.IsMatch(e.NewTextValue, passwordRegex));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            ((Entry)sender).BackgroundColor = IsValid ? Color.LightGreen : Color.LightPink;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}