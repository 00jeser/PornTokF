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
    class SubscribesViewModel : INotifyPropertyChanged
    {
        private string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "subscribe");

        private ObservableCollection<string> _subs = new ObservableCollection<string>();
        public ObservableCollection<string> Subs 
        {
            get => _subs;
            set 
            {
                _subs = value;
                OnPropertyChanged(nameof(Subs));
            }
        }

        public Command Click { get; set; }


        public SubscribesViewModel() 
        {
            Subs = new ObservableCollection<string>(File.ReadAllLines(p).ToList());
            Click = new Command((name) => 
            {
                var ls = File.ReadAllLines(p).ToList();
                var ds = File.ReadAllText(p);
                if (ls.Contains(name.ToString()))
                {
                    ls.Remove(name.ToString());
                }
                else 
                {
                    ls.Add(name.ToString());
                }
                string s = string.Join("\n", ls);
                File.WriteAllText(p, s);
            });
        }


        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
