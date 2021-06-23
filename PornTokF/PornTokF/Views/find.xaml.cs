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
    public partial class find : CarouselPage, INavigateAction
    {

        public find()
        {
            InitializeComponent();
            FindE += (s) =>
            {
                findEntry.Text = s;
            };
            (BindingContext as FindViewModel).sourse = this;
            SuggestBox.IsVisible = false;
            SuggestBox.ItemsSource = Finder.TagsList;
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

        private void findEntry_Focused(object sender, FocusEventArgs e)
        {
            SuggestBox.IsVisible = true;
        }

        private void findEntry_Unfocused(object sender, FocusEventArgs e)
        {
            SuggestBox.IsVisible = false;
        }

        private void SuggestBox_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var ctx = e?.SelectedItem?.ToString();
            if (ctx != null)
            {
                if (findEntry.Text == null || findEntry.Text == "")
                findEntry.Text = ctx;
                else
                findEntry.Text = (string.Join(" ", findEntry.Text?.Split().Reverse()?.Skip(1).Reverse()) + " " + ctx + " ").Trim();
                SuggestBox.SelectedItem = null;
            }
        }

        private async void findEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SuggestBox.ItemsSource = Finder.TagsList.Where(x => x.Contains(e.NewTextValue.Split().Last()));
        }

        public void OnNavigate()
        {
            CurrentPage = Children.First();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ImageScrollView.ScrollToAsync(0,0,true);
        }
    }
}