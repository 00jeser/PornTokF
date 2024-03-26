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
using Microsoft.Maui.Controls;
using Microsoft.Maui;


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
            await Task.Delay(1000);
            var ts = Liker.GenetateTags();
            Photos = new ObservableCollection<PhotoViewModel>((await Finder.FindPostsByNameAsync(ts, "3")).Select(x => new PhotoViewModel(x)));
        }
        private ObservableCollection<PhotoViewModel> photos { get; set; } = new ObservableCollection<PhotoViewModel>(new PhotoViewModel[] { new PhotoViewModel(new Post() { File_url="" }), new PhotoViewModel(new Post() { File_url = "" }) });
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
                await Task.Run(add);
            }
        }

        public int _n = 0;
        public async void add()
        {
            if (Photos.First().Photo.File_url == null)
                return;
            string tags = Liker.GenetateTags();
            //(new Random()).Next(10000).ToString()
            int n = new Random().Next(4, 6);
            //var c = (new Random()).Next(Math.Min(Finder.GetPostCounts(tags) / n, 1000)).ToString();
            var c = 3;
            var ls = await Finder.FindPostsByNameAsync(tags, n.ToString(), "0");
            // TODO Xamarin.Forms.Device.RuntimePlatform is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            if (Device.RuntimePlatform == Device.UWP) 
            {
                var nPs = Photos.ToList();
                foreach (var v in ls)
                    nPs.Add(new PhotoViewModel(v));
                Photos = new ObservableCollection<PhotoViewModel>(nPs);
            }
            else
            {
                foreach (var p in ls)
                {
                    Photos.Add(new PhotoViewModel(p));
                }
                OnPropertyChanged("Photos");
            }
        }


        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
