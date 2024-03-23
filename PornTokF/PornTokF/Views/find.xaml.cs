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
            SuggestBox.ItemsSource = HistorySevice.History;
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
            if (e.ScrollY < 100)
                toUpBtn.IsVisible = false;
            else
                toUpBtn.IsVisible = true;
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
                if (string.IsNullOrWhiteSpace(findEntry.Text))
                    findEntry.Text = ctx;
                else
                    findEntry.Text = (string.Join(" ", findEntry.Text?.Split().Reverse()?.Skip(1).Reverse()) + " " + ctx + " ").Trim();
                SuggestBox.SelectedItem = null;
            }
        }

        private async void findEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(findEntry.Text))
            {
                IEnumerable<string> res = new List<string>();
                await Task.Run(() =>
                     res = Finder.TagsList.Where(x => x.Contains(e.NewTextValue.Split().Last()))
                );
                SuggestBox.ItemsSource = res;
            }
            else
            {
                SuggestBox.ItemsSource = HistorySevice.History;
            }
        }

        public void OnNavigate()
        {
            CurrentPage = Children.First();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ImageScrollView.ScrollToAsync(0, 0, true);
        }

        private void Instruction_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Как использовать теги", "-tag удаляет результаты с тегом \n\n" +
                "tag* добавляет результаты с тегами, которые частично состоят из tag\n\n" +
                "( tag1 ~ tag2 ) находи посты которые содержат хотя бы один из тегов",
                "Понял");
        }

        private async void toUpBtn_Clicked(object sender, EventArgs e)
        {
            await ImageScrollView.ScrollToAsync(0, 0, true);
        }
    }
}