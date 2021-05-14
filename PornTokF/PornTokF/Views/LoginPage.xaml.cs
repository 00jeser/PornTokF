using PornTokF.ViewModels;
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
    public partial class LoginPage : CarouselPage
    {
        public LoginPage()
        {
            InitializeComponent();
            f();
        }
        async void f()
        {
            var v = new List<Models.Post>();
            v.Add(new Models.Post() { File_url = "https://wimg.rule34.xxx/images/4162/d9de634a1ff32acdca514c413ca1686b.jpeg" });
            ItemsSource = v;
        }
    }
}