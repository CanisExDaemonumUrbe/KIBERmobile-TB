using KIBERmobile.Models;
using KIBERmobile.ViewModels;
using KIBERmobile.Views.MainGroup;

namespace KIBERmobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FeedPage : ContentPage
{
    private readonly FeedVM _vm;

    public FeedPage()
    {
        BindingContext = _vm = new FeedVM();
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.OnAppearing();
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