using FFImageLoading.Svg.Forms;
using PornTokF.Models;
using PornTokF.Services;
using PornTokF.ViewModels;
using PornTokF.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms.Xaml;

namespace PornTokF
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //findShell.Icon = new SvgImageSource(ImageSource.FromResource("find.svg"), 0, 0, true);
        }

    }
}
