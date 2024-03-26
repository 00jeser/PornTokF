using PornTokF.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PornTokF
{
    public partial class App : Application
    {

        public App()
        {
            Services.Liker.Init();
            Services.UserIDCaching.Init();
            Services.Subscriber.Init();
            InitializeComponent();

            MainPage = new AppShell();
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

        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            
        }
    }
}
