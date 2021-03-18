using System;
using System.IO;
using LAP1.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromHex("#002f51"),
                BarTextColor = Color.White,
            };
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
