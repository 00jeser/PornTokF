using PornTokF.Models;
using PornTokF.Services;
using PornTokF.ViewModels;
using PornTokF.Views;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PornTokF
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //findShell.Icon = new SvgImageSource(ImageSource.FromResource("find.svg"), 0, 0, true);
        }
        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            // Cancel any back navigation.
            if (args.Source == ShellNavigationSource.Pop)
            {
                (CurrentPage as INavigateAction).OnNavigate();
                args.Cancel();
            }
        }

    }
}
