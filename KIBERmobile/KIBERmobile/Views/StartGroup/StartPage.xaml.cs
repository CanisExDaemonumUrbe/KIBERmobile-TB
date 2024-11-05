using CommunityToolkit.Maui.Views;
using KIBERmobile.ViewModels;

namespace KIBERmobile.Views.StartGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StartPage : ContentPage
{
    private readonly StartVM _vm;

    // private static Boolean _isOpen;

    public StartPage()
    {
        BindingContext = _vm = new StartVM();
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        // if (!_isOpen)
        // {
        //     _isOpen = true;
        //     
        //     base.OnAppearing();
        //     await _vm.RefreshView();
        //
        //     if (DarkOverlay.IsVisible)
        //     {
        //         await DarkOverlay.FadeTo(0, 25);   // Анимация исчезновения затемнения
        //         DarkOverlay.IsVisible = false; 
        //     }
        //     
        //     if (!_vm.IsAuthorized)
        //     {
        //         OpenLoginPopup();
        //     }
        //     else
        //     {
        //         Application.Current.MainPage = new AppTabbedPage();
        //     }
        //
        //     _isOpen = false;
        // }


        base.OnAppearing();
        await _vm.RefreshView();

        if (!_vm.IsAuthorized)
        {
            var result = await this.ShowPopupAsync(new LoginPopup(), CancellationToken.None);

            if (result is bool boolResult)
                if (boolResult)
                    Application.Current.MainPage = new AppTabbedPage();
        }
        else
        {
            Application.Current.MainPage = new AppTabbedPage();
        }
    }

    // private async void OpenLoginPopup()
    // {
    //     DarkOverlay.IsVisible = true;
    //     await DarkOverlay.FadeTo(0.5, 25);
    //     
    //     await Navigation.PushModalAsync(new LoginPopup());
    // }
}