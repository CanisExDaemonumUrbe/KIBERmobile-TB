using CommunityToolkit.Maui.Views;
using KIBERmobile.ViewModels;

namespace KIBERmobile.Views.StartGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoginPopup : Popup
{
    private readonly LoginVM _vm;

    public LoginPopup()
    {
        BindingContext = _vm = new LoginVM();
        InitializeComponent();
        _vm.LoginVMOnAppearing();
    }

    private async void LoginClickedAsync(object? sender, EventArgs e)
    {
        await _vm.OnLoginClickedAsync();

        if (!_vm.IsAuthorized) return;
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(true, cts.Token);
    }

}