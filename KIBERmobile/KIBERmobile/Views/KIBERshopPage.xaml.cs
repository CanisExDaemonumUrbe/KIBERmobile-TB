using KIBERmobile.Models;
using KIBERmobile.ViewModels;
using KIBERmobile.Views.KIBERshopGroup;

namespace KIBERmobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class KIBERshopPage : ContentPage
{
    private readonly KIBERshopVM _vm;

    public KIBERshopPage()
    {
        BindingContext = _vm = new KIBERshopVM();
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        _vm.OnAppearing();
        base.OnAppearing();
    }

    private async void GoToHistoryPageAsync(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new KiberonHistoryPage());
    }

    private async void GoToItemPageAsync(object? sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Item;
        if (selectedItem != null)
        {
            await Navigation.PushAsync(new ItemPage(selectedItem.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}