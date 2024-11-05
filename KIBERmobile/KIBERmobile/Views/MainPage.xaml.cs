using KIBERmobile.Models;
using KIBERmobile.ViewModels;
using KIBERmobile.Views.MainGroup;

namespace KIBERmobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage : ContentPage
{
    private readonly FeedVM _vm;
    
    public MainPage()
    {
        BindingContext = _vm = new FeedVM();
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.OnAppearing();
    }

    private async void GoToResidentPageAsync(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResidentPage());
    }

    private async void GoToParentPageAsync(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ParentPage());
    }

    private async void GoToNewsPageAsync(object? sender, SelectionChangedEventArgs e)
    {
        var selectedNews = e.CurrentSelection.FirstOrDefault() as News;
        if (selectedNews != null)
        {
            await Navigation.PushAsync(new NewsPage(selectedNews.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}