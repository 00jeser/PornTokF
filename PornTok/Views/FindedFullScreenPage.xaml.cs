using PornTokF.ViewModels;

namespace PornTokF.Views;

[QueryProperty(nameof(ViewModel), "ViewModel")]
public partial class FindedFullScreenPage : ContentPage, INavigateAction
{
    public FindViewModel ViewModel
    {
        set
        {
            BindingContext = value;
        }
    }
    public FindedFullScreenPage()
    {
        InitializeComponent();
    }

    public void OnNavigate()
    {
        Shell.Current.GoToAsync("//find");
    }
}