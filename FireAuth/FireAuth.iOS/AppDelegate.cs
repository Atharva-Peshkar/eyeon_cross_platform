using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.RemoteConfig;
using Foundation;
using UIKit;

namespace FireAuth.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            GoogleVisionBarCodeScanner.iOS.Initializer.Init();
            // Temporary work around for bug on Firebase Library
            // https://github.com/xamarin/GoogleApisForiOSComponents/issues/368
            Firebase.Core.App.Configure();
            RemoteConfig.SharedInstance.ConfigSettings = new RemoteConfigSettings();
            return base.FinishedLaunching(app, options);
        }
    }
}