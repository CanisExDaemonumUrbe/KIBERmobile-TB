using CommunityToolkit.Maui.Views;
using KIBERmobile.ViewModels;

namespace KIBERmobile.Views.SupportGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SupportPopup : Popup
{
    private readonly SupportFormVM _vm;

    private readonly string _formType;

    public SupportPopup(string formType)
    {
        _formType = formType;

        BindingContext = _vm = new SupportFormVM(formType);
        InitializeComponent();
        UpdateComponent();
    }

    private void UpdateComponent()
    {
        Content.Spacing = 10;
        Content.VerticalOptions = _vm.DeviceIdiom == DeviceIdiom.Tablet
            ? LayoutOptions.Center
            : LayoutOptions.End;
        Content.Margin = new Thickness(0);
        Content.Padding = _vm.DeviceIdiom == DeviceIdiom.Tablet
            ? new Thickness(_vm.DisplayWidth / 10, 0, _vm.DisplayWidth / 10, 0)
            : new Thickness(10);

        switch (_formType)
        {
            case "request":

                NameRow.IsVisible = true;
                PhoneRow.IsVisible = true;
                EmailRow.IsVisible = true;
                MessageRow.IsVisible = true;
                break;

            case "invite":

                NameRow.IsVisible = true;
                SecondNameRow.IsVisible = true;
                PhoneRow.IsVisible = true;
                EmailRow.IsVisible = true;
                break;

            case "callback":

                NameRow.IsVisible = true;
                PhoneRow.IsVisible = true;
                break;

            case "tech":

                EmailRow.IsVisible = true;
                MessageRow.IsVisible = true;
                break;
        }

        _vm.OnAppearing();
    }

    private async void CancelClicked(object? sender, EventArgs e)
    {
        await CloseAsync();
    }

    private async void SendClicked(object? sender, EventArgs e)
    {
        await _vm.SendFormAsync(_formType);

        if (!_vm.IsSent) return;

        NameRow.IsVisible = false;
        SecondNameRow.IsVisible = false;
        PhoneRow.IsVisible = false;
        EmailRow.IsVisible = false;
        MessageRow.IsVisible = false;

        SendButton.IsVisible = false;
        CancelButton.IsVisible = false;

        SuccessButton.IsVisible = true;
    }
}