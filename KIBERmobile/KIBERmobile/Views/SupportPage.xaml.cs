using CommunityToolkit.Maui.Views;
using KIBERmobile.ViewModels;
using KIBERmobile.Views.SupportGroup;

namespace KIBERmobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SupportPage : ContentPage
{
    private readonly SupportVM _vm;

    public SupportPage()
    {
        BindingContext = _vm = new SupportVM();
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.OnAppearing();
    }

    private async void LeaveRequest(object? sender, EventArgs e)
    {
        await this.ShowPopupAsync(new SupportPopup("request"));
    }

    private async void RequestCallback(object? sender, EventArgs e)
    {
        //await this.ShowPopupAsync(new SupportFormPopup("callback"));
        await this.ShowPopupAsync(new SupportPopup("callback"));
    }

    private async void InviteFriend(object? sender, EventArgs e)
    {
        //await this.ShowPopupAsync(new SupportFormPopup("invite"));
        await this.ShowPopupAsync(new SupportPopup("invite"));
    }

    private async void TechSupportRequest(object? sender, EventArgs e)
    {
        //await this.ShowPopupAsync(new SupportFormPopup("tech"));
        await this.ShowPopupAsync(new SupportPopup("tech"));
    }
}