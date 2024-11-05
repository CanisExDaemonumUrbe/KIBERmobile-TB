using KIBERmobile.ViewModels;
using KIBERmobile.Views.MainGroup.ProfileGroup;

namespace KIBERmobile.Views.MainGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ResidentPage : ContentPage
{
    private readonly ResidentVM _vm;

    public ResidentPage()
    {
        BindingContext = _vm = new ResidentVM();
        InitializeComponent();
    }

    private async void GoToProfilePageAsync(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }
}