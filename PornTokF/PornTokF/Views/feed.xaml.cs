﻿using PornTokF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PornTokF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class feed : CarouselPage
    {
        public feed()
        {
            InitializeComponent();
            Init();
        }
        public async void Init() 
        {
            var a = 0;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var s = (sender as Label).Text;
            await Shell.Current.GoToAsync("//find");
            find.FindF(s);
        }
    }
}