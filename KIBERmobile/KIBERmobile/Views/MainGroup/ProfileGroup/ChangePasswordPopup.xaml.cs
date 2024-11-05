using CommunityToolkit.Maui.Views;
using KIBERmobile.ViewModels;

namespace KIBERmobile.Views.MainGroup.ProfileGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ChangePasswordPopup : Popup
{
    private readonly ChangePasswordVM _vm;

    public ChangePasswordPopup()
    {
        BindingContext = _vm = new ChangePasswordVM();
        InitializeComponent();
        UpdateComponent();
        _vm.OnAppearing();
    }

    private void UpdateComponent()
    {
        Wrapper.Padding = new Thickness(10);

        Content.Spacing = 10;
        Content.VerticalOptions = _vm.DeviceIdiom == DeviceIdiom.Tablet
            ? LayoutOptions.Center
            : LayoutOptions.End;
        Content.Margin = new Thickness(0);
        Content.Padding = _vm.DeviceIdiom == DeviceIdiom.Tablet
            ? new Thickness(_vm.DisplayWidth / 10, 0, _vm.DisplayWidth / 10, 0)
            : new Thickness(0);
    }

    private async void CancelClickedAsync(object? sender, EventArgs e)
    {
        await CloseAsync();
    }

    private async void SentClickedAsync(object? sender, EventArgs e)
    {
        await _vm.SaveChangedPasswordAsync();

        if (!_vm.IsSent) return;

        ForgotPassRow.IsVisible = false;
        OldPassRaw.IsVisible = false;
        NewPassRaw.IsVisible = false;
        DoubleNewPassRaw.IsVisible = false;

        SendButton.IsVisible = false;
        CancelButton.IsVisible = false;

        SuccessButton.IsVisible = true;
    }
}