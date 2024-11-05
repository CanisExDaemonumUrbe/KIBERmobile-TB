using KIBERmobile.ViewModels;
using KIBERmobile.Views.MainGroup.ProfileGroup;

namespace KIBERmobile.Views.MainGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ParentPage : ContentPage
{
    private readonly ParentVM _vm;

    public ParentPage()
    {
        BindingContext = _vm = new ParentVM();
        InitializeComponent();
    }

    private async void GoToProfilePageAsync(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }
}