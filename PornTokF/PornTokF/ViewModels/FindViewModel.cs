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
            ViewPhoto = new ObservableCollection<PhotoViewModel>() { new PhotoViewModel(p) };
    }

    private ObservableCollection<PhotoViewModel> _viewPhoto;

    public ObservableCollection<PhotoViewModel> ViewPhoto
    {
        get { return _viewPhoto; }
        set { _viewPhoto = value; OnPropertyChanged(nameof(ViewPhoto)); }
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
        Photos = new ObservableCollection<PhotoFindViewModel>((await Finder.FindPostsAsync(FindString, "59")).ToList().Select(x => new PhotoFindViewModel(this) { Photo = x }));
    }

    private bool canAdd = true;

    public async void Add()
    {
        if (canAdd)
        {
            canAdd = false;
            foreach (var i in await Finder.FindPostsAsync(FindString, "59", (++pid).ToString()))
            {
                await Task.Delay(100);
                Photos.Add(new PhotoFindViewModel(this) { Photo = i });
                OnPropertyChanged(nameof(Photos));
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
