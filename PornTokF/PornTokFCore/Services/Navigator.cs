using PornTokF.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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
