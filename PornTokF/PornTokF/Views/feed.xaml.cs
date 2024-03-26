using PornTokF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PornTokF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class feed : CarouselPage, INavigateAction
    {
        public feed()
        {
            InitializeComponent();
        }

        public void OnNavigate()
        {
            CurrentPage = Children.First();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var s = (sender as Label).Text;
            await Shell.Current.GoToAsync("//find");
            find.FindF(s);
        }
    }
}