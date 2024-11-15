using CommunityToolkit.Maui.Views;
using KIBERmobile.ViewModels;
using KIBERmobile.Views.GuestGroup;

namespace KIBERmobile.Views.StartGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class StartPage : ContentPage
{
    private readonly StartVM _vm;

    public StartPage()
    {
        BindingContext = _vm = new StartVM();
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        
        base.OnAppearing();
        await _vm.RefreshView();
        
        
        if (!_vm.IsAuthorized)
        {
            var result = await this.ShowPopupAsync(new LoginPopup(), CancellationToken.None);

            if (result is bool boolResult)
            {
                if (boolResult)
                    Application.Current.MainPage = new AppTabbedPage();
            }
            else
            {
                if (result as string == "guest")
                {
                    App.Current.MainPage = new GuestPage();
                }
            }
                
        }
        else
        {
            Application.Current.MainPage = new AppTabbedPage();
        }
    }
    
}