using static KIBERmobile.LocalPackages.DataProducer;
using System.Diagnostics;

namespace KIBERmobile.ViewModels;

internal class ItemVM : BaseViewModel
{
    private int _itemId;
    private string _title;
    private string _description;
    private int _price;
    private ImageSource _image;

    public int Id { get; set; }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public int Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    public ImageSource Image
    {
        get => _image;
        set => SetProperty(ref _image, value);
    }

    public int ItemId
    {
        get => _itemId;
        set
        {
            _itemId = value;
            LoadItemById(value);
        }
    }

    public ItemVM(int itemId)
    {
        ItemId = itemId;

        PageTitle = "Что тут у нас?";

        OpenShopTermsCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.ShopTerms));
    }

    public Command OpenShopTermsCommand { get; }

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

    private ImageSource SetImage(string imageUrl)
    {
        ImageSource result;

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

    private async void LoadItemById(int itemId)
    {
        try
        {
            var item = await ShopController.GetItemAsync(itemId);

            Id = item.Id;
            Title = SetProperty(item.Title);
            Description = SetProperty(item.Description);
            Price = item.Price;
            Image = SetImage(item.BigImages);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}