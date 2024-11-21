using KIBERmobile.Models;
using KIBERmobile.ViewModels;
using KIBERmobile.Views.MainGroup;
using KIBERmobile.Views.MainGroup.ProfileGroup;

namespace KIBERmobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage : ContentPage
{
    private readonly MainVM _vm;

    public MainPage()
    {
        BindingContext = _vm = new MainVM();
        InitializeComponent();
    }

    private async void GoToProfilePageAsync(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }
}