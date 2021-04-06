using PornTokF.Models;
using PornTokF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PornTokF.ViewModels
{
    class FindViewModel : INotifyPropertyChanged
    {
        private int pid = 0;
        private ObservableCollection<Post> _photos { get; set; } = new ObservableCollection<Post>();
        public ObservableCollection<Post> Photos
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
            Photos = new ObservableCollection<Post>();
            pid = 0;
            Photos = new ObservableCollection<Post>(await Finder.FindVideosAsync(FindString, "59"));
        }

        private bool canAdd = true;

        public async void Add()
        {
            if (canAdd)
            {
                canAdd = false;
                foreach (var i in await Finder.FindVideosAsync(FindString, "59", (++pid).ToString()))
                {
                    await Task.Delay(100);
                    Photos.Add(i);
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
