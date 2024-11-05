using static KIBERmobile.LocalPackages.DataProducer;
using System.Diagnostics;

namespace KIBERmobile.ViewModels;

internal class NewsVM : BaseViewModel
{
    private int _newsId;
    private string _title;
    private string _anons;
    private string _description;
    private string _date;
    private ImageSource _image;
    private string _type;

    public int Id { get; set; }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Anons
    {
        get => _anons;
        set => SetProperty(ref _anons, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public ImageSource Image
    {
        get => _image;
        set => SetProperty(ref _image, value);
    }

    public string Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }

    public int NewsId
    {
        get => _newsId;
        set
        {
            _newsId = value;
            LoadNewsById(value);
        }
    }

    public NewsVM(int newsId)
    {
        NewsId = newsId;

        PageTitle = "А вы знали?";

        OpenFeedTermsCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.FeedTerms));
    }

    public Command OpenFeedTermsCommand { get; }

    private string SetProperty(string rawProperty)
    {
        string resultProperty;

        try
        {
            if (IsNullOrEmpty(rawProperty))
            {
                resultProperty = string.Empty;
            }
            else
            {
                resultProperty = rawProperty.Replace("<br />", "\n");
                resultProperty = resultProperty.Replace("&quot;", "\"");
            }
        }
        catch
        {
            resultProperty = "Упс... Что-то пошло нет так";
        }

        return resultProperty;
    }

    private string SetDateTime(string rawDateTime)
    {
        try
        {
            return $"Дата публикации: {rawDateTime}";
        }
        catch
        {
            return "Упс! Что-то пошло не так";
        }
    }

    private ImageSource SetImage(string imageUrl)
    {
        ImageSource result = null;

        try
        {
            result = new UriImageSource()
            {
                Uri = new Uri($"{imageUrl}"),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(0, 0, 10, 0)
            };
        }
        catch
        {
            result = new UriImageSource()
            {
                Uri = new Uri($"https://apitest.na4u.ru/gate/v1/static/image/user/error/error"),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(0, 0, 10, 0)
            };
        }

        return result;
    }

    private async void LoadNewsById(int newsId)
    {
        try
        {
            var news = await FeedController.GetItemAsync(newsId);

            Id = news.Id;
            Title = SetProperty(news.Title);
            Anons = SetProperty(news.Anons);
            Description = SetProperty(news.Description);
            Date = SetDateTime(news.Date);
            Image = SetImage(news.BigImages);
            Type = SetProperty(news.Type);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}