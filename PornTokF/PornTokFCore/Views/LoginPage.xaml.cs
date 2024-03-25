using PornTokF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PornTokF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage, INavigateAction
    {
        public LoginPage()
        {
            InitializeComponent();
            //PushAsync(new LoginPages.mainLogin());
        }

        public void OnNavigate()
        {
            
        }
    }
}