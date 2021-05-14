﻿using PornTokF.Models;
using PornTokF.Views;
using PornTokF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PornTokF.ViewModels
{
    class PhotoFindViewModel : INotifyPropertyChanged
    {
        public Command Open { get; set; }
        private Post photo;
        public Post Photo { get => photo; set { photo = value; OnPropertyChanged(nameof(Photo)); } }

        public PhotoFindViewModel(FindViewModel fvm)
        {
            Open = new Command(() =>
            {
                fvm.OpenImage(Photo);
                //FindNavigationPage.page.openImage(new PhotoViewModel(Photo));
            });
        }

        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    class FindViewModel : INotifyPropertyChanged
    {
        private int pid = 0;
        private ObservableCollection<PhotoFindViewModel> _photos { get; set; } = new ObservableCollection<PhotoFindViewModel>();
        public ObservableCollection<PhotoFindViewModel> Photos
        {
            get { return _photos; }
            set
            {
                if (_photos != value)
                {
                    _photos = value;
                    OnPropertyChanged("Photos");
                }
            }
        }

        public find sourse;
        public void OpenImage(Post p)
        {
            sourse.OpenImage();
            var t1 = ViewPhotos.Where(x => x.Photo.Id == p.Id);
            ViewPhoto = t1.First();
        }

        private ObservableCollection<PhotoViewModel> _viewPhotos;
        public ObservableCollection<PhotoViewModel> ViewPhotos
        {
            get { return _viewPhotos; }
            set { _viewPhotos = value; OnPropertyChanged(nameof(ViewPhotos)); }
        }
        private PhotoViewModel _viewPhoto;
        public PhotoViewModel ViewPhoto
        {
            get { return _viewPhoto; }
            set 
            { 
                _viewPhoto = value; 
                OnPropertyChanged(nameof(ViewPhoto)); 
            }
        }



        private string _findString;
        public string FindString
        {
            get
            {
                return _findString;
            }
            set
            {
                _findString = value;
                OnPropertyChanged(nameof(FindString));
            }
        }

        public Command Find { get; set; }
        public FindViewModel()
        {
            Find = new Command(async () =>
            {
                Init();
            });
            Init();
        }

        public async void Init()
        {
            Photos = new ObservableCollection<PhotoFindViewModel>();
            pid = 0;
            var t = (await Finder.FindPostsAsync(FindString, "59")).ToList();
            Photos = new ObservableCollection<PhotoFindViewModel>(t.Select(x => new PhotoFindViewModel(this) { Photo = x }));
            ViewPhotos = new ObservableCollection<PhotoViewModel>(t.Select(x => new PhotoViewModel(x)));
        }

        private bool canAdd = true;

        public async void Add()
        {
            if (canAdd)
            {
                canAdd = false;
                var x = await Finder.FindPostsAsync(FindString, "59", (++pid).ToString());
                if (Device.RuntimePlatform == Device.UWP)
                {
                    var nVPs = ViewPhotos.ToList();
                    foreach (var i in x)
                    {
                        Photos.Add(new PhotoFindViewModel(this) { Photo = i });
                        nVPs.Add(new PhotoViewModel(i));
                    }
                    OnPropertyChanged(nameof(Photos));
                    ViewPhotos = new ObservableCollection<PhotoViewModel>(nVPs);
                    OnPropertyChanged(nameof(ViewPhotos));
                }
                else
                {
                    foreach (var i in x)
                    {
                        Photos.Add(new PhotoFindViewModel(this) { Photo = i });
                        ViewPhotos.Add(new PhotoViewModel(i));
                    }
                    }
                canAdd = true;
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
