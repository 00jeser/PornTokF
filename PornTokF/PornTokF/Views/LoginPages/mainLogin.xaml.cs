using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PornTokF.Views.LoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mainLogin : ContentPage, INavigateAction
    {
        public mainLogin()
        {
            InitializeComponent();
        }

        public void OnNavigate()
        {
            
        }

        private void like_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//LoginLike");
        }

        private void sub_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//LoginSubscribe");
        }
    }
}