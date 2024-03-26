using PornTokF.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PornTokF.Services
{
    public static class Navigator
    {
        public static Command FindTag = new Command((s) =>
        {
            Shell.Current.GoToAsync("//find");
            find.FindF(s.ToString());
        });
    }
}
