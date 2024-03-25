using PornTokF.Models;
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
    class LikesPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PhotoViewModel> likesList = new ObservableCollection<PhotoViewModel>();
        public ObservableCollection<PhotoViewModel> LikesList
        {
            get { return likesList; }
            set { likesList = value; OnPropertyChanged(nameof(LikesList)); }
        }

        private PhotoViewModel currentPhoto;
        public PhotoViewModel CurrentPhoto
        {
            get { return currentPhoto; }
            set
            {
                if (value == LikesList.Last())
                {
                    AddNext5();
                }
                currentPhoto = value;
                OnPropertyChanged(nameof(CurrentPhoto));
            }
        }

        private bool isUpdated;
        public bool IsUpdated
        {
            get { return isUpdated; }
            set { isUpdated = value; OnPropertyChanged(nameof(IsUpdated)); }
        }




        private List<int> likesIds;

        public LikesPageViewModel()
        {
            likesIds = Liker.likes.Select(x => int.Parse(x.Split('~')[0])).Reverse().ToList();
            AddNext5();
            Update = new Command(x =>
            {
                LikesList.Clear();
                likesIds = Liker.likes.Select(y => int.Parse(y.Split('~')[0])).Reverse().ToList();
                AddNext5();
            });
        }

        private bool needAdd = true;
        public async void AddNext5()
        {
            if (needAdd)
            {
                needAdd = false;
                var x = Math.Min(5, likesIds.Count);
                for (int i = 0; i < x; i++)
                {
                    LikesList.Add(new PhotoViewModel(await Finder.FindPostsByIdsAsync(likesIds[0])));
                    likesIds.RemoveAt(0);
                    await Task.Delay(1000);
                }
                needAdd = true;
            }
            IsUpdated = false;
        }

        public Command Update { get; set; }

        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
