using PornTokF.Models;
using PornTokF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PornTokF.ViewModels
{
    class SubscribeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PhotoViewModel> photos { get; set; }
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

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }

        public Command Refresh { get; set; }
        public SubscribeViewModel()
        {
            Refresh = new Command(() => 
            {
                init();
            });
            init();
        }
        public async void init()
        {
            //%28+user%3abot+~+user%3abob+%29+sort%3acreated_at%3adesc
            try
            {
                string s = "%28+";
                s += string.Join("+~+", Services.Subscriber.SubList.Select(x => $"user%3a{x}"));
                s += "+%29";
                //s += "+sort%3acreated_at%3adesc";
                Photos = new ObservableCollection<PhotoViewModel>((await Finder.FindPostsByNameAsync(s, "100")).Select(x => new PhotoViewModel(x)));
                IsRefreshing = false;
            }
            catch (Exception)
            {

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
