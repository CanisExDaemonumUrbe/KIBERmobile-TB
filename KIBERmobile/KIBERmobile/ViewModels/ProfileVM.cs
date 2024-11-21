using static KIBERmobile.LocalPackages.DataProducer;

namespace KIBERmobile.ViewModels;

internal class ProfileVM : BaseViewModel
{
    private bool _isPortfolioExist;

    public bool IsPortfolioExist
    {
        get => _isPortfolioExist;
        set => SetProperty(ref _isPortfolioExist, value);
    }

    private ImageSource _residentAvatar;

    private string _avatarDummy;
    
    public ImageSource ResidentAvatar
    {
        get => _residentAvatar;
        set => SetProperty(ref _residentAvatar, value);
    }
    public string AvatarDummy
    {
        get => _avatarDummy;
        set => SetProperty(ref _avatarDummy, value);
    }
    

    //Блок с внутренними переменными
    #region

    private string _residentFio = "Ученик кибершколы";

    private string _residentBirthday = "Загрузка...";

    private string _residentNumber = "Загрузка";
    
    private string _email = "Загрузка...";
    
    private string _parentFio = "Родитель ученика";

    private string _parentPhone = "Загрузка...";

    private string _startDate = "Загрузка...";

    private string _kiberoneBalance = "Загрузка...";

    private string _residentCity = "Загрузка...";

    private string _residentLocation = "Загрузка...";

    private string _residentGroup = "Загрузка...";

    private string _residentModule = "Загрузка...";
    
    #endregion
    
    //Блок с внешними переменными
    #region
    
    public string ResidentFIO
    {
        get => _residentFio;
        set => SetProperty(ref _residentFio, value);
    }
    public string ResidentBirthday
    {
        get => _residentBirthday;
        set => SetProperty(ref _residentBirthday, value);
    }
    public string ResidentNumber
    {
        get => _residentNumber;
        set => SetProperty(ref _residentNumber, value);
    }
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }
    public string ParentFIO
    {
        get => _parentFio;
        set => SetProperty(ref _parentFio, value);
    }
    public string ParentPhone
    {
        get => _parentPhone;
        set => SetProperty(ref _parentPhone, value);
    }
    public string StartDate
    {
        get => _startDate;
        set => SetProperty(ref _startDate, value);
    }
    public string KiberoneBalance
    {
        get => _kiberoneBalance;
        set => SetProperty(ref _kiberoneBalance, value);
    }
    public string ResidentCity
    {
        get => _residentCity;
        set => SetProperty(ref _residentCity, value);
    }
    public string ResidentLocation
    {
        get => _residentLocation;
        set => SetProperty(ref _residentLocation, value);
    }
    public string ResidentGroup
    {
        get => _residentGroup;
        set => SetProperty(ref _residentGroup, value);
    }
    public string ResidentModule
    {
        get => _residentModule;
        set => SetProperty(ref _residentModule, value);
    }
    
    #endregion


    public ProfileVM()
    {
        PageTitle = "Профиль";
        IsPortfolioExist = !IsNullOrEmpty(Profile.ResidentPortfolioLink);

        ResidentAvatar = SetImage(Profile.ResidentAvatarPath, "big");
        AvatarDummy = SetAvatarDummy(Profile.ResidentFIO);

        ResidentFIO = SetString(Profile.ResidentFIO);
        ResidentBirthday = SetDateTime(Profile.ResidentBirthday);
        ResidentNumber = SetString(Profile.ResidentPhoneNumber);
        Email = SetString(Profile.ResidentEmail);
        ParentFIO = SetString(Profile.ResidentParentsName);
        ParentPhone = SetString(Profile.ResidentParentsPhone);
        
        StartDate = SetDateTime(Profile.ResidentStartLearning);
        KiberoneBalance = SetString(Profile.ResidentKiberonBalance);
        ResidentCity = SetString(Profile.CityName);
        ResidentLocation = SetString(Profile.LocationName);
        ResidentGroup = SetString(Profile.GroupName);
        ResidentModule = SetString(Profile.ModuleName);
        
    }

    private ImageSource SetImage(string imageUrl, string size)
    {
        ImageSource result;

        try
        {
            result = new UriImageSource()
            {
                Uri = new Uri($"{imageUrl}/{size}"),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(0, 0, 10, 0)
            };
        }
        catch
        {
            result = new UriImageSource()
            {
                Uri = new Uri($"https://kiber-one.fun/gate/v1/static/image/user/error/error"),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(0, 0, 10, 0)
            };
        }

        return result;
    }

    private string SetAvatarDummy(string residentFIO)
    {
        var result = "";

        try
        {
            var splittedFIO = residentFIO.Split(" ");
            result = splittedFIO.Aggregate(result, (current, name) => current + name[..1]);
        }
        catch
        {
            result = "K1";
        }

        return result;
    }

    public async Task GoToProfileSiteAsync()
    {
        await OpenLinkAsync(Profile.ResidentPortfolioLink);
    }
}