using PornTokF.Models;
using PornTokF.Services;
using PornTokF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Xamarin.Forms;


namespace PornTokF.ViewModels
{
    class RecomendViewModel : INotifyPropertyChanged
    {
        public RecomendViewModel()
        {
            init();
        }
        public async void init()
        {
            Photos = new ObservableCollection<PhotoViewModel>((await Finder.FindPostsAsync(Liker.GenetateTags(), "3")).Select(x => new PhotoViewModel(x)));
        }
        private ObservableCollection<PhotoViewModel> photos { get; set; } = new ObservableCollection<PhotoViewModel>(new PhotoViewModel[] { new PhotoViewModel(new Post()), new PhotoViewModel(new Post()) });
        public ObservableCollection<PhotoViewModel> Photos
        {
            get { return photos; }
            set
            {
                if (photos != value)
                {
                    photos = value;
                    OnPropertyChanged("Photos");
                }
            }
        }
        public PhotoViewModel CurrentItem
        {
            set
            {
                AddAsync(value);
            }
        }

        private async void AddAsync(object value)
        {
            if (Photos.Count < 6)
                add();
            else
            if (Photos?.Count != 0 && (value == Photos?.Last() || value == Photos?[Photos.Count - 2] || value == Photos?[Photos.Count - 6] || value == Photos?[Photos.Count - 4]))
            {
                Task.Run(add);
            }
        }

        public int n = 0;
        public async void add()
        {
            string tags = Liker.GenetateTags();
#if DEBUG
            var sn = n;
            n++;
            Acr.UserDialogs.UserDialogs.Instance.Toast($"Add({sn}) - {tags}", new TimeSpan(0,0,0,1,0));
#endif
            //(new Random()).Next(10000).ToString()
            var c = (await Finder.GetPostCounts(tags) / 5).ToString();
            var ls = await Finder.FindPostsAsync(tags, "5", c);
            foreach (var p in ls)
            {
                Photos.Add(new PhotoViewModel(p));
            }
            OnPropertyChanged("Photos");
#if DEBUG
            Acr.UserDialogs.UserDialogs.Instance.Toast($"Added({sn})", new TimeSpan(0, 0, 0, 0, 300));
#endif
        }


        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
