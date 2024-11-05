using KIBERmobile.ViewModels;
using KIBERmobile.Views.StartGroup;

namespace KIBERmobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MenuPage : ContentPage
{
    private readonly MenuVM _vm;

    public MenuPage()
    {
        BindingContext = _vm = new MenuVM();
        InitializeComponent();

        LogOutButton.Clicked += LogOutClickedAsync!;
    }

    private async void LogOutClickedAsync(object sender, EventArgs e)
    {
        if (await DisplayAlert(
                "Подтвердите выход",
                "Вы действительно хотите выйти из аккаунта?",
                "Выйти",
                "Остаться")
           )
        {
            await _vm.LogOut();
            Application.Current.MainPage = new NavigationPage(new StartPage());
        }
    }
}