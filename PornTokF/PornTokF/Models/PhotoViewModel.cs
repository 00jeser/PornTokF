using HtmlAgilityPack;
using PornTokF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PornTokF.Models
{
    public class stringContainer
    {
        public string value { get; set; }
    }
    public class PhotoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private Aspect _aspect;
        public Aspect aspect { get { return _aspect; } set { _aspect = value; OnPropertyChanged("aspect"); } }
        private Post _photo;
        public Post Photo
        {
            get { return _photo; }
            set
            {
                var p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "subscribe");
                _photo = value;
                if (value != null && value.Id != null)
                {
                    Liked = Liker.FindIt(value?.Id);

                    Creator = UserIDCaching.LoadUserNameOnlyFromCashe(Photo.Creator_id);
                    if (Creator != "")
                    {
                        try
                        {
                            var ls = File.ReadAllLines(p).ToList();
                            SubscribeString = ls.Contains(Creator) ? "Отписаться" : "Подписатся";

                        }
                        catch (FileNotFoundException)
                        {
                            File.WriteAllText(p, Creator);
                        }

                    }
                }
                OnPropertyChanged("Photo");
            }
        }
        public List<stringContainer> tagsList { get; set; } = new List<stringContainer>();
        //Sun Mar 28 08:48:53 +0000 2021
        public string date
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(new string(Photo.Created_at.Skip(4).ToArray()), "MMM dd hh:mm:ss +ffff yyyy", CultureInfo.InvariantCulture).ToString("dd MMM yyyy HH:mm");
                }
                catch (Exception)
                {
                    return Photo.Created_at;
                }
            }
        }
        private bool _moreIsVisible = false;
        public bool MoreIsVisible { get { return _moreIsVisible; } set { _moreIsVisible = value; OnPropertyChanged("MoreIsVisible"); } }
        private bool _liked = false;
        public bool Liked { get { return _liked; } set { _liked = value; OnPropertyChanged("Liked"); OnPropertyChanged("NotLiked"); } }
        public bool NotLiked { get { return !_liked; } set { _liked = !value; OnPropertyChanged("NotLiked"); OnPropertyChanged("Liked"); } }
        private string _creator = "";
        public string Creator { get { return _creator; } set { _creator = value; OnPropertyChanged("Creator"); } }
        private string _subscribeString = " ";
        public string SubscribeString { get { return _subscribeString; } set { _subscribeString = value; OnPropertyChanged("SubscribeString"); } }

        public PhotoViewModel p => this;

        public Command Like { get; set; }
        public Command Web { get; set; }
        public Command Aspect { get; set; }
        public Command Share { get; set; }
        public Command Subscribe { get; set; }
        public Command More { get; set; }
        public PhotoViewModel(Post post) : this()
        {
            Photo = post;
        }
        public PhotoViewModel()
        {
            var p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "subscribe");
            aspect = Xamarin.Forms.Aspect.AspectFit;
            Web = new Command(async () =>
            {
                Device.OpenUri(new Uri(Photo.Sample_url));
            });
            More = new Command(async x =>
            {
                MoreIsVisible = !MoreIsVisible;
                tagsList = Photo?.Tags?.Split(' ').Where(y => y.Trim() != string.Empty).Select(y => new stringContainer() { value = y }).ToList();
                OnPropertyChanged("tagsList");
                if (_creator == "")
                {
                    Creator = await UserIDCaching.LoadUserNameAsync(Photo.Creator_id, Photo.Id);
                    try
                    {
                        var ls = File.ReadAllLines(p).ToList();
                        SubscribeString = ls.Contains(Creator) ? "Отписаться" : "Подписатся";
                        OnPropertyChanged("Creator");

                    }
                    catch (FileNotFoundException)
                    {
                        File.WriteAllText(p, Creator);
                    }
                }
            });
            Subscribe = new Command(x =>
            {
                try
                {
                    var ls = File.ReadAllLines(p).ToList();
                    if (ls.Contains(Creator))
                    {
                        ls.Remove(Creator);
                    }
                    else
                    {
                        ls.Add(Creator);
                    }
                    SubscribeString = ls.Contains(Creator) ? "Отписаться" : "Подписатся";
                    File.WriteAllLines(p, ls.ToArray());
                }
                catch (FileNotFoundException)
                {
                    File.WriteAllText(p, Creator);
                }
            });
            Share = new Command(async () =>
            {
                await Xamarin.Essentials.Share.RequestAsync("https://rule34.xxx/index.php?page=post&s=view&id=" + Photo.Id);
            });
            Aspect = new Command(async () =>
            {
                await Task.Run(() => aspect = aspect == Xamarin.Forms.Aspect.AspectFill ? Xamarin.Forms.Aspect.AspectFit : Xamarin.Forms.Aspect.AspectFill);
            });
            Like = new Command(async () => await Task.Run(() =>
            {
                if (Liker.FindIt(Photo))
                {
                    Liker.DeleteIt(Photo);
                    Liked = false;
                }
                else
                {
                    Liked = true;
                    Liker.LikeIt(Photo);
                }
            }));
            //OnPropertyChanged("Liked"); OnPropertyChanged("NotLiked");
        }

    }
}
