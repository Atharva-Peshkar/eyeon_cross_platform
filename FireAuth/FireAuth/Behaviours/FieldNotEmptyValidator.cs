using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FireAuth
{
    public class FieldNotEmptyValidator : Behavior<Entry>
    {

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            ((Entry)sender).BackgroundColor = IsValid ? Color.LightGreen : Color.LightPink;
            IsValid = (e.NewTextValue.Length>0);
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