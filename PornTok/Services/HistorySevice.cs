using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace PornTokF.Services
{
    public static class HistorySevice
    {
        private static string p = Path.Combine(FileSystem.AppDataDirectory, "history");
        private static ObservableCollection<string> _history = null;
        public static ObservableCollection<string> History
        {
            get
            {
                if (_history == null)
                {
                    if (File.Exists(p))
                    {
                        _history = new ObservableCollection<string>(File.ReadAllLines(p));
                    }
                    else
                    {
                        _history = new ObservableCollection<string>();
                        File.Create(p);
                    }
                }
                return _history;
            }
        }
        public static async void addHistory(string s)
        {
            History.Remove(s);
            History.Insert(0, s);
            await File.WriteAllLinesAsync(p, _history);
        }
    }
}
