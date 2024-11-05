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

    private string _startDate;

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

    public string StartDate
    {
        get => _startDate;
        set => SetProperty(ref _startDate, value);
    }


    //Блок с данными резидента - вынести в отдельный модуль?

    #region

    private string residentFIO = "Ученик кибершколы";

    private string residentBirthday = "Загрузка...";

    private string residentCity = "Загрузка...";

    private string residentLocation = "Загрузка...";

    private string residentGroup = "Загрузка...";


    //------------------------------------//

    public string ResidentFIO
    {
        get => residentFIO;
        set => SetProperty(ref residentFIO, value);
    }

    public string ResidentBirthday
    {
        get => residentBirthday;
        set => SetProperty(ref residentBirthday, value);
    }

    public string ResidentCity
    {
        get => residentCity;
        set => SetProperty(ref residentCity, value);
    }

    public string ResidentLocation
    {
        get => residentLocation;
        set => SetProperty(ref residentLocation, value);
    }

    public string ResidentGroup
    {
        get => residentGroup;
        set => SetProperty(ref residentGroup, value);
    }

    #endregion


    //Блок с данными родителя

    #region

    private string parentFIO = "Родитель ученика";

    private string parentPhone = "Загрузка...";

    private string parentEmail = "Загрузка...";


    //------------------------------------//

    public string ParentFIO
    {
        get => parentFIO;
        set => SetProperty(ref parentFIO, value);
    }

    public string ParentPhone
    {
        get => parentPhone;
        set => SetProperty(ref parentPhone, value);
    }

    public string ParentEmail
    {
        get => parentEmail;
        set => SetProperty(ref parentEmail, value);
    }

    #endregion


    public ProfileVM()
    {
        PageTitle = "Профиль";
        IsPortfolioExist = !IsNullOrEmpty(Profile.ResidentPortfolioLink);

        ResidentAvatar = SetImage(Profile.ResidentAvatarPath, "big");
        AvatarDummy = SetAvatarDummy(Profile.ResidentFIO);
        StartDate = SetDateTime(Profile.ResidentStartLearning);

        ResidentFIO = SetProperty(Profile.ResidentFIO);
        ResidentBirthday = SetDateTime(Profile.ResidentBirthday);
        ResidentCity = SetProperty(Profile.CityName);
        ResidentLocation = SetProperty(Profile.LocationName);
        ResidentGroup = SetProperty(Profile.GroupName);

        ParentFIO = SetProperty(Profile.ResidentParentsName);
        ParentPhone = SetProperty(Profile.ResidentParentsPhone);
        ParentEmail = SetProperty(Profile.ResidentEmail);
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

    private string SetProperty(string? rawProperty)
    {
        var resultProperty = "Не указано";

        try
        {
            if (rawProperty != null) resultProperty = rawProperty;
        }
        catch
        {
            resultProperty = "Упс... Что-то пошло нет так";
        }

        return resultProperty;
    }

    private string SetDateTime(string rawDateTime)
    {
        string resultDateTime;

        try
        {
            resultDateTime = IsNullOrEmpty(rawDateTime)
                ? "Не указано"
                : DateTime.Parse(rawDateTime).ToString("dd.MM.yyyy");
        }
        catch
        {
            resultDateTime = "Упс! Что-то пошло не так";
        }

        return resultDateTime;
    }

    public async Task GoToProfileSiteAsync()
    {
        await OpenLinkAsync(Profile.ResidentPortfolioLink);
    }
}