using static KIBERmobile.LocalPackages.DataProducer;

namespace KIBERmobile.ViewModels;

public class SupportVM : BaseViewModel
{
    private string? _cityName;

    private string? _cityEmail;

    private string? _cityPhone;


    public string? CityName
    {
        get => _cityName;
        set => SetProperty(ref _cityName, value);
    }

    public string? CityEmail
    {
        get => _cityEmail;
        set => SetProperty(ref _cityEmail, value);
    }

    public string? CityPhone
    {
        get => _cityPhone;
        set => SetProperty(ref _cityPhone, value);
    }

    public SupportVM()
    {
        CityName = SetProperty(Profile.CityName);

        CityPhone = SetProperty(Profile.CityManagerPhoneNumber);
        CityEmail = SetProperty(Profile.CityManagerEmail);

        RefreshViewCommand = new Command(RefreshView);
    }

    public Command RefreshViewCommand { get; }

    private string SetProperty(string? rawProperty)
    {
        var resultProperty = "Не указано";

        try
        {
            if (rawProperty != null) resultProperty = rawProperty;
        }
        catch
        {
            resultProperty = "Упс! Что-то пошло нет так";
        }

        return resultProperty;
    }

    private void RefreshView()
    {
        //CityPhone = SetProperty(Profile.CityManagerPhoneNumber);
        //CityEmail = SetProperty(Profile.CityManagerEmail);

        //IsBusy = false;
    }

    public void OnAppearing()
    {
        //IsBusy = true;
    }
}