using KIBERmobile.ViewModels;

namespace KIBERmobile.Views.KIBERshopGroup;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class KiberonHistoryPage : ContentPage
{
    private readonly KiberonHistoryVM _vm;

    public KiberonHistoryPage()
    {
        BindingContext = _vm = new KiberonHistoryVM();
        InitializeComponent();

        Header.WidthRequest = _vm.DisplayWidth / _vm.DisplayDensity - 20;
        Footer.WidthRequest = _vm.DisplayWidth / _vm.DisplayDensity - 20;

        Header.HorizontalOptions = LayoutOptions.Center;
        Footer.HorizontalOptions = LayoutOptions.Center;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.OnAppearing();
    }
}