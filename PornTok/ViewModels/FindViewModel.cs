using PornTokF.Models;
using PornTokF.Views;
using PornTokF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PornTokF.ViewModels
{
    public class PhotoFindViewModel : INotifyPropertyChanged
    {
        public Command Open { get; set; }
        private Post photo;
        public Post Photo { get => photo; set { photo = value; OnPropertyChanged(nameof(Photo)); } }

        public PhotoFindViewModel(FindViewModel fvm)
        {
            Open = new Command((object o) =>
            {
                Shell.Current.GoToAsync("//FindedFullScreenPage", new Dictionary<string, object>() {
                    { "ViewModel", fvm },
                });
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
    public class FindViewModel : INotifyPropertyChanged
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
        public void OpenImage(Post pst)
        {
            //sourse.OpenImage();
            foreach (var p in ViewPhotos)
                if (p.Photo.Id == pst.Id)
                    ViewPhoto = p;
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
        private string FindQuery => FindString + " sort:" +
            Sorting switch { 0 => "updated", 1 => "source", _ => "random" } 
            + " score:>="+MinScore;

        private int _sorting = 2;
        public int Sorting
        {
            get => _sorting;
            set { _sorting = value; OnPropertyChanged(nameof(Sorting)); }
        }

        private int _minScore = 5;
        public int MinScore
        {
            get => _minScore;
            set { _minScore = value; OnPropertyChanged(nameof(MinScore)); }
        }

        private string _newStatusLable;
        public string NewStatusLable
        {
            get => _newStatusLable;
            set { _newStatusLable = value; OnPropertyChanged(nameof(NewStatusLable));}
        }

        public Command Find { get; set; }
        public FindViewModel()
        {
            ViewPhotos = new ObservableCollection<PhotoViewModel>();
            Photos = new ObservableCollection<PhotoFindViewModel>();
            Find = new Command(async () =>
            {
                HistorySevice.addHistory(FindString);
                ViewPhoto = ViewPhotos.First();
                await Init();
            });
            Init();
        }

        public async Task Init()
        {
            //FindString = FindQuery;
            pid = 0;
            var t = (await Finder.FindPostsByNameAsync(FindQuery, "59")).ToList();
            // TODO Xamarin.Forms.Device.RuntimePlatform is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            if (Device.RuntimePlatform == Device.UWP)
            {
                Photos = new ObservableCollection<PhotoFindViewModel>(t.Select(x => new PhotoFindViewModel(this) { Photo = x }));
                ViewPhotos = new ObservableCollection<PhotoViewModel>(t.Select(x => new PhotoViewModel(x)));
            }
            else
            {
                Photos.Clear();
                ViewPhotos.Clear();
                foreach (var p in t)
                {
                    Photos.Add(new PhotoFindViewModel(this) { Photo = p });
                    ViewPhotos.Add(new PhotoViewModel(p));
                }
                OnPropertyChanged(nameof(ViewPhotos));
                OnPropertyChanged(nameof(Photos));
            }
        }

        private bool canAdd = true;

        public async void Add()
        {
            if (canAdd)
            {
                NewStatusLable = "Поиск...";
                canAdd = false;
                var x = await Finder.FindPostsByNameAsync(FindQuery, "59", (++pid).ToString());
                // TODO Xamarin.Forms.Device.RuntimePlatform is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
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
                if (x.Count == 0)
                    NewStatusLable = "Конец";
                else
                    NewStatusLable = "";
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
