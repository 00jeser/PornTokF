using PornTokF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PornTokF.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public string SafeString
        {
            get
            {
                return SafeModeService.SafeString;
            }
            set
            {
                string newVal = value.Replace("-","");
                SafeModeService.SafeString = newVal;
                OnPropertyChanged(nameof(SafeModeService.SafeString));
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
