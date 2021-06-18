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
        private string _userName;
        public string UserName
        {
            get => (string)GetValue(UserNameProperty);
            set
            {
                SetValue(UserNameProperty, value);
            }
        }
        public static readonly BindableProperty UserNameProperty = BindableProperty.Create(
            nameof(UserName),
            typeof(string),
            typeof(UserSubscribe),
            "def",
            propertyChanging: (BindableObject bindable, object oldValue, object newValue) =>
            {
                (bindable as UserSubscribe).ChangeUserName((string)newValue);
            }
         );
        public UserSubscribe()
        {
            InitializeComponent();
            SubButtonLabel.Text = "";

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

        public void ChangeUserName(string value)
        {

                _userName = value;
                UserNameLabel.Text = value;
                if (value == "")
                    SubButtonLabel.Text = "";
                else
                    SubButtonLabel.Text = Subscriber.Contains(value) ? "Отписаться" : "Подписаться";
                _userName = value;
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