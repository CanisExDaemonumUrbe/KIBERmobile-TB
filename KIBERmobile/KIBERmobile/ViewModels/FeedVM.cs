using KIBERmobile.Models;
using KIBERmobile.Views;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace KIBERmobile.ViewModels;

internal class FeedVM : BaseViewModel
{
    private bool _isLoaded;
    

    private ref ObservableCollection<News> _news => ref FeedController.Collection;
    public ObservableCollection<News> News { get; }

    public FeedVM()
    {
        RefreshViewCommand = new Command(async () => await RefreshViewAsync(Profile.CityId));
        News = new ObservableCollection<News>();
        OpenFeedTermsCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.FeedTerms));
    }

    public Command OpenFeedTermsCommand { get; }
    public Command RefreshViewCommand { get; }

    private async Task RefreshViewAsync(int cityId)
    {
        if (FeedController.Status != 200)
        {
            News.Clear();

            await FeedController.LoadDataAsync(cityId);

            for (var i = _news.Count - 1; i >= 0; i--) News.Add(_news[i]);

            FeedController.Status = 200;

            _isLoaded = true;
        }

        IsBusy = false;
    }

    public void OnAppearing()
    {
        if (!_isLoaded) IsBusy = true;
    }
}