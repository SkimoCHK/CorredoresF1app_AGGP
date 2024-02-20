using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CorredoresF1app_AGGP.View;

namespace CorredoresF1app_AGGP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new DriverView();
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
