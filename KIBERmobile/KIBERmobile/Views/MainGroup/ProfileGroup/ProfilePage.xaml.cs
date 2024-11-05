using CommunityToolkit.Maui.Views;
using KIBERmobile.ViewModels;

namespace KIBERmobile.Views.MainGroup.ProfileGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ProfilePage : ContentPage
{
    private readonly ProfileVM _vm;

    public ProfilePage()
    {
        BindingContext = _vm = new ProfileVM();
        InitializeComponent();
    }

    private async void ChangePasswordClickedAsync(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new ChangePasswordPopup());
    }

    private async void GoToPortfolioAsync(object? sender, EventArgs e)
    {
        if (await DisplayAlert(
                "Подтвердите переход",
                "Вы собираетесь перейти на сторонний ресурс, администрация KIBERone не несёт ответственности за его работоспособность",
                "Перейти",
                "Остаться")
           )
            await _vm.GoToProfileSiteAsync();
    }
}