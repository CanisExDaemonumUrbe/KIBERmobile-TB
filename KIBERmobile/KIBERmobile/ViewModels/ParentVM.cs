using static KIBERmobile.LocalPackages.DataProducer;
using KIBERmobile.Views;

namespace KIBERmobile.ViewModels;

public class ParentVM : BaseViewModel
{
    //Карточка персональной информации

    #region

    private ImageSource _residentAvatar;

    private string _parentFio;

    private string _parentPhone;

    private string _parentEmail;

    private string _parentAvatarDummy;

    //------------------------------------//

    public ImageSource ResidentAvatar
    {
        get => _residentAvatar;
        set => SetProperty(ref _residentAvatar, value);
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

    public string ParentEmail
    {
        get => _parentEmail;
        set => SetProperty(ref _parentEmail, value);
    }

    public string ParentAvatarDummy
    {
        get => _parentAvatarDummy;
        set => SetProperty(ref _parentAvatarDummy, value);
    }

    #endregion


    //Карточка оплаты

    #region

    private string _paymentDate = "Загрузка...";

    //------------------------------------//

    public string PaymentDate
    {
        get => _paymentDate;
        set => SetProperty(ref _paymentDate, value);
    }

    #endregion


    public ParentVM()
    {
        GoToSitePaymentPageCommand = new Command(async () => await GoToThePaymentSite());

        ResidentAvatar = SetResidentAvatar(Profile.ResidentAvatarPath, "big");
        ParentFIO = SetProperty(Profile.ResidentParentsName);
        ParentPhone = SetProperty(Profile.ResidentParentsPhone);
        ParentEmail = SetProperty(Profile.ResidentEmail);
        ParentAvatarDummy = SetAvatarDummy(Profile.ResidentParentsName);

        PaymentDate = SetDateTime(Profile.ResidentPayDay);
    }

    public Command GoToSitePaymentPageCommand { get; }

    private async Task<bool> GoToThePaymentSite()
    {
        IsBusy = true;

        string link;

        try
        {
            link = Profile.CityPaymentLink;

            await Browser.OpenAsync(link, BrowserLaunchMode.SystemPreferred);
            return true;
        }
        catch
        {
            link = "https://kiber-one.com/oplata/";
            await Browser.OpenAsync(link, BrowserLaunchMode.SystemPreferred);
            return false;
        }
        finally
        {
            IsBusy = false;
        }
    }

    private ImageSource SetResidentAvatar(string avatartUrl, string size)
    {
        ImageSource result = null;

        try
        {
            result = new UriImageSource()
            {
                Uri = new Uri($"{avatartUrl}/{size}"),
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

    private string SetAvatarDummy(string residentFIO)
    {
        var result = "";

        try
        {
            string[] splittedFIO = residentFIO.Split(" ");
            foreach (var name in splittedFIO) result += name[..1];
        }
        catch
        {
            result = "K1";
        }

        return result;
    }

    private string SetProperty(string rawProperty)
    {
        string resultProperty;

        try
        {
            resultProperty = IsNullOrEmpty(rawProperty) ? "Не указано" : rawProperty;
        }
        catch
        {
            resultProperty = "Упс! Что-то пошло нет так";
        }

        return resultProperty;
    }

    private string SetDateTime(string rawDateTime)
    {
        string resultDateTtime;

        try
        {
            resultDateTtime = IsNullOrEmpty(rawDateTime)
                ? "Не указано"
                : DateTime.Parse(rawDateTime).ToString("dd.MM.yyyy");
        }
        catch
        {
            resultDateTtime = "Упс! Что-то пошло не так";
        }


        return resultDateTtime;
    }
}