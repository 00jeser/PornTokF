using PornTokF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PornTokF.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSubscribe : ContentView
    {
        public UserSubscribe()
        {
            InitializeComponent();
            SubButtonLabel.Text = "";
            
        }
        private string _userName;
        public string UserName 
        {
            get => _userName;
            set 
            {
                UserNameLabel.Text = value;
                SubButtonLabel.Text = Subscriber.Contains(value)? "Отписаться":"Подписаться";
                _userName = value;
            }
        }

        public Color UserLabelTextColor
        {
            set => UserNameLabel.TextColor = value;
            get => UserNameLabel.TextColor;
        }
        public Color ButtonLabelTextColor
        {
            set => SubButtonLabel.TextColor = value;
            get => SubButtonLabel.TextColor;
        }
        public float FontSize 
        {
            set 
            {
                SubButtonLabel.FontSize = value;
                UserNameLabel.FontSize = value;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (Subscriber.Contains(_userName)) 
            {
                Subscriber.Remove(_userName);
                SubButtonLabel.Text = "Подписаться";
            }
            else
            {
                Subscriber.Add(_userName);
                SubButtonLabel.Text = "Отписаться";
            }
        }
    }
}