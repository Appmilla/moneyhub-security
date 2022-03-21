using System;
using MoneyhubSecurityDemo.Shell;
using MoneyhubSecurityDemo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyhubSecurityDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new WelcomeView();
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
