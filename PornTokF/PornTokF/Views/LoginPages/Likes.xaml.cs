﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PornTokF.Views.LoginPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Likes : ContentPage, INavigateAction
	{
		public Likes ()
		{
			InitializeComponent ();
		}

        public void OnNavigate()
        {
			Shell.Current.GoToAsync("//LoginMain");
        }
    }
}