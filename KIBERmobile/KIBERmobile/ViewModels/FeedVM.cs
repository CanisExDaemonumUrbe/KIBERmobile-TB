using KIBERmobile.Models;
using KIBERmobile.Views;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace KIBERmobile.ViewModels;

internal class FeedVM : BaseViewModel
{
    private bool _isLoaded;
    private int _dayForPay;
    private Color? _dayForPayColor;

    public int DayForPay
    {
        get => _dayForPay;
        set => SetProperty(ref _dayForPay, value);
    }

    public Color? DayForPayColor
    {
        get => _dayForPayColor;
        set => SetProperty(ref _dayForPayColor, value);
    }

    private ref ObservableCollection<News> _news => ref FeedController.Collection;
    public ObservableCollection<News> News { get; }

    public FeedVM()
    {
        var tDayForPay = (int)(DateTime.Parse(Profile.ResidentPayDay) - DateTime.Now).TotalDays + 1;

        PageTitle = "Дней до оплаты: ";
        DayForPay = tDayForPay < 0 ? 0 : tDayForPay;
        DayForPayColor = DayForPay <= 10 ? StatusColors.Error : StatusColors.OK;

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