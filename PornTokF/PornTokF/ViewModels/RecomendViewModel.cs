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
            Photos = new ObservableCollection<PhotoViewModel>((await Finder.FindVideosAsync("", "3", new Random().Next(10000).ToString())).Select(x => new PhotoViewModel(x)));
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
            if (Photos?.Count != 0 && (value == Photos?.Last() || value == Photos?[Photos.Count - 2]))
            {
                await Task.Run(add);
            }
        }

        public async void add()
        {
            foreach (var p in await Finder.FindVideosAsync("", "10", (new Random()).Next(10000).ToString()))
            {
                Photos.Add(new PhotoViewModel(p));
            }
            OnPropertyChanged("Photos");
        }


        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
