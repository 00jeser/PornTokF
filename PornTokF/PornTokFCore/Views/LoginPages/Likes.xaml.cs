﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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