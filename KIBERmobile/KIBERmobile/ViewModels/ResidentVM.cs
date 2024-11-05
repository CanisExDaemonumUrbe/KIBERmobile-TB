using System;
using static KIBERmobile.LocalPackages.DataProducer;
using KIBERmobile.Views;

namespace KIBERmobile.ViewModels;

public class ResidentVM : BaseViewModel
{
    //Карточка персональной информации - вынести в отдельный модуль?

    #region

    private ImageSource _residentAvatar;

    private string _residentFio = "Ученик кибершколы";

    private string _residentAge = "6-14";

    private int _residentKiberoneBalance = 0;

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

    public int ResidentKiberoneBalance
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

    public ResidentVM()
    {
        VideoExist = !string.IsNullOrEmpty(Profile.ModuleVideoLink);

        ResidentAvatar = SetImageSource(Profile.ResidentAvatarPath, "big");
        ResidentFIO = SetProperty(Profile.ResidentFIO);
        ResidentAge = SetResidentAge(Profile.ResidentBirthday);
        ResidentKiberoneBalance = int.Parse(SetProperty(Profile.ResidentKiberonBalance.ToString()));
        ResidentCity = SetProperty(Profile.CityName);
        ResidentLocation = SetProperty(Profile.LocationName);
        ResidentGroup = SetProperty(Profile.GroupName);
        ResidentModule = SetProperty(Profile.ModuleName);
        ResidentAvatarDummy = SetAvatarDummy(Profile.ResidentFIO);

        ModuleGreetingMessage = SetModuleGreetingMessage(Profile.ResidentFIO, Profile.ModuleName);
        ModuleImage = SetImageSource(Profile.ModuleImage, "big");
        ModuleInfoVideoLink = SetModuleInfoVideo(Profile.ModuleVideoLink);
        ModuleProgressMessage = SetModuleProgressMessage(Profile.ModuleCurrentLesson, Profile.ModuleCountLesson);
    }

    //Общие методы
    private string SetProperty(string rawProperty)
    {
        string resultProperty;

        try
        {
            resultProperty = IsNullOrEmpty(rawProperty) ? "Не указано" : rawProperty;
        }
        catch
        {
            resultProperty = "Упс... Что-то пошло нет так";
        }

        return resultProperty;
    }

    //------------------------------------//


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
}