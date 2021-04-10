using PornTokF.Models;
using PornTokF.Services;
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
    public partial class find : CarouselPage
    {

        public find()
        {
            InitializeComponent();
            FindE += (s) =>
            {
                findEntry.Text = s;
            };
            (BindingContext as FindViewModel).sourse = this;
        }
        public delegate void findD(string s);
        public static event findD FindE;
        public static void FindF(string s) => FindE?.Invoke(s);

        private async void OnScrolled(object sender, ScrolledEventArgs e)
        {
            ScrollView scrollView = sender as ScrollView;
            double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height - 10;

            if (scrollingSpace <= e.ScrollY)
            {
                (this.BindingContext as FindViewModel).Add();
            }
        }
        public void OpenImage()
        {
            CurrentPage = Children.Last();
        }
    }
}