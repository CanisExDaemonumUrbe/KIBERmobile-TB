using static KIBERmobile.LocalPackages.DataProducer;

namespace KIBERmobile.ViewModels;

public class MainVM : BaseViewModel
{
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
    
    
    //Карточка информации о модуле
    #region

    private bool _videoExist;

    private string _moduleGreetingMessage = "Hello, ученик!\nПока что ты ничего не проходишь";

    private ImageSource _moduleImage;

    private HtmlWebViewSource _moduleInfoVideoLink = new()
    {
        Html =
            @$"<style> body{{ margin: 0; }} </style> <body> <iframe class=""inframe"" width=""100%"" height=""100%""  src=""https://www.youtube.com/embed/AVeYydaDww0?si=izgk-yv-mzX1iq0l"" frameborder=""0"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"" allowfullscreen></body>"
    };

    private string _moduleProgressMessage = "Тут будет отображаться прогресс модуля";

    //------------------------------------//

    public bool VideoExist
    {
        get => _videoExist;
        set => SetProperty(ref _videoExist, value);
    }

    public string ModuleGreetingMessage
    {
        get => _moduleGreetingMessage;
        set => SetProperty(ref _moduleGreetingMessage, value);
    }

    public ImageSource ModuleImage
    {
        get => _moduleImage;
        set => SetProperty(ref _moduleImage, value);
    }

    public HtmlWebViewSource ModuleInfoVideoLink
    {
        get => _moduleInfoVideoLink;
        set => SetProperty(ref _moduleInfoVideoLink, value);
    }

    public string ModuleProgressMessage
    {
        get => _moduleProgressMessage;
        set => SetProperty(ref _moduleProgressMessage, value);
    }

    #endregion
    
    //Карточка персональной информации
    #region

    private ImageSource _residentAvatar;

    private string _residentFio = "Ученик кибершколы";

    private string _residentAge = "6-14";

    private string _residentKiberoneBalance = "Загрузка...";

    private string _residentCity = "Загрузка...";

    private string _residentLocation = "Загрузка...";

    private string _residentGroup = "Загрузка...";

    private string _residentModule = "Загрузка...";

    private string _residentAvatarDummy = "K1";

    //------------------------------------//


    public ImageSource ResidentAvatar
    {
        get => _residentAvatar;
        set => SetProperty(ref _residentAvatar, value);
    }

    public string ResidentFIO
    {
        get => _residentFio;
        set => SetProperty(ref _residentFio, value);
    }

    public string ResidentAge
    {
        get => _residentAge;
        set => SetProperty(ref _residentAge, value);
    }

    public string ResidentKiberoneBalance
    {
        get => _residentKiberoneBalance;
        set => SetProperty(ref _residentKiberoneBalance, value);
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

    public string ResidentAvatarDummy
    {
        get => _residentAvatarDummy;
        set => SetProperty(ref _residentAvatarDummy, value);
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

    public MainVM()
    {
        var tDayForPay = (int)(DateTime.Parse(Profile.ResidentPayDay) - DateTime.Now).TotalDays + 1;
        PageTitle = "Дней до оплаты: ";
        DayForPay = tDayForPay < 0 ? 0 : tDayForPay;
        DayForPayColor = DayForPay <= 10 ? StatusColors.Error : StatusColors.OK;
        
        GoToSitePaymentPageCommand = new Command(async () => await GoToThePaymentSite());
        
        VideoExist = !string.IsNullOrEmpty(Profile.ModuleVideoLink);

        ResidentAvatar = SetImageSource(Profile.ResidentAvatarPath, "big");
        ResidentFIO = SetString(Profile.ResidentFIO);
        ResidentAge = SetResidentAge(Profile.ResidentBirthday);
        ResidentKiberoneBalance = SetString(Profile.ResidentKiberonBalance);
        ResidentCity = SetString(Profile.CityName);
        ResidentLocation = SetString(Profile.LocationName);
        ResidentGroup = SetString(Profile.GroupName);
        ResidentModule = SetString(Profile.ModuleName);
        ResidentAvatarDummy = SetAvatarDummy(Profile.ResidentFIO);

        ModuleGreetingMessage = SetModuleGreetingMessage(Profile.ResidentFIO, Profile.ModuleName);
        ModuleImage = SetImageSource(Profile.ModuleImage, "big");
        ModuleInfoVideoLink = SetModuleInfoVideo(Profile.ModuleVideoLink);
        ModuleProgressMessage = SetModuleProgressMessage(Profile.ModuleCurrentLesson, Profile.ModuleCountLesson);
        
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
    
    //Методы карточки резидента

    private string SetAvatarDummy(string residentFIO)
    {
        var result = "";

        try
        {
            string[] splittedFIO = residentFIO.Split(" ");
            result = splittedFIO.Aggregate(result, (current, name) => current + name[..1]);
        }
        catch
        {
            result = "K1";
        }

        return result;
    }

    private string SetResidentAge(string rawBirthday)
    {
        string residentAge;

        try
        {
            var birthday = DateTime.Parse(rawBirthday);

            var age = (int)(DateTime.Now - birthday).TotalDays / 365;

            residentAge = age.ToString();
        }
        catch
        {
            residentAge = "Упс... Что-то пошло не так";
        }

        return residentAge;
    }

    //------------------------------------//


    //Методы карточки модуля
    private string SetModuleGreetingMessage(string residentFIO, string moduleTitle)
    {
        string result;
        string name;

        try
        {
            name = residentFIO.Split(' ')[1];
        }
        catch
        {
            name = "ученик";
        }

        try
        {
            result = string.IsNullOrEmpty(moduleTitle)
                ? $"Hello, {name}! Как дела?\nСейчас ты ничего не изучаешь"
                : $"Hello, {name}! Как дела?\nСейчас ты изучаешь модуль:\n{moduleTitle}";
        }
        catch
        {
            result = $"Hello, {name}! Как дела?\nДавай посмотрим, какой модуль ты проходишь!";
        }

        return result;
    }

    private HtmlWebViewSource SetModuleInfoVideo(string? videoLink)
    {
        var link = "";

        try
        {
            link = videoLink;
        }
        catch
        {
            link = "https://www.youtube.com/embed/AVeYydaDww0?si=izgk-yv-mzX1iq0l";
        }

        var html =
            @$"<style> body{{ margin: 0; }} </style> <body> <iframe class=""iframe"" width=""100%"" height=""100%""  src=""{link}"" frameborder=""0"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"" allowfullscreen></body>";

        return new HtmlWebViewSource() { Html = html };
    }

    private string SetModuleProgressMessage(int lessonNumber, int lessonsCount)
    {
        string result;

        try
        {
            result = lessonsCount == -1
                ? $"Здесь будет отображаться прогресс модуля!"
                : $"Ты прошёл {lessonNumber} из {lessonsCount} уроков модуля!";
        }
        catch (Exception e)
        {
            result = $"Ты на верном пути!";
        }

        return result;
    }

    //------------------------------------//
    
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