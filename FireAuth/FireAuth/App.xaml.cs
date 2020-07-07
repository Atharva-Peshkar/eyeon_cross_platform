using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FireAuth
{
    public partial class App : Application
    {
        static TodoItemDatabase database;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static TodoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoItemDatabase();
                }
                return database;
            }
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
