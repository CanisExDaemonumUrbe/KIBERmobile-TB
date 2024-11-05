using KIBERmobile.Views;


namespace KIBERmobile.ViewModels;

public class StartVM : BaseViewModel
{
    private bool _isAuthorized;

    public bool IsAuthorized
    {
        get => _isAuthorized;
        protected set => SetProperty(ref _isAuthorized, value);
    }

    private protected string _login;
    private protected string _password;

    public string Login
    {
        get => _login;
        set => SetProperty(ref _login, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public StartVM()
    {
        RefreshViewCommand = new Command(async () => await RefreshView());
    }

    public Command RefreshViewCommand { get; }


    private async Task<bool> CheckServerStatusAsync()
    {
        var isOnline = false;

        await ServerController.CheckServerStatusAsync();

        var status = ServerController.ServerStatus;

        if (status == "Online")
        {
            SetStatusOK("Сервер доступен!");
            isOnline = true;
        }
        else
        {
            SetStatusWarning("Сервер находится на техническом обслуживании! Попробуйте позже!");
        }

        return isOnline;
    }

    private async Task<bool> LoadPassAsync(string login, string password)
    {
        var isLoaded = false;

        await PassController.GetPassAsync(login, password);

        var status = PassController.Status;


        switch (status)
        {
            case 200:
                SetStatusOK("Авторизация пройдена!");
                isLoaded = true;
                break;
            case 200403:
                SetStatusWarning("Неверный логин или пароль!");
                break;
            case 200404:
                SetStatusWarning("Пользователя не существует!");
                break;
            case -1:
                SetLostConnect_Error();
                break;
            case 403:
                SetStatusError("Обнаружена устаревшая версия приложения! Обновите до актуальной!");
                break;
            default:
                SetUndefined_Error();
                break;
        }

        return isLoaded;
    }

    private async Task<bool> LoadProfileAsync(int userId)
    {
        SetStatusOK("Загрузка данных...");

        var isLoaded = false;

        await ProfileController.LoadProfileDataAsync(userId);

        var status = ProfileController.Status;

        switch (status)
        {
            case 200:
                SetStatusOK("Данные успешно загружены!");
                isLoaded = true;
                break;
            case -1:
                SetLostConnect_Error();
                break;
            case 403:
                SetStatusError("Обнаружена устаревшая версия приложения! Обновите до актуальной!");
                break;
            default:
                SetUndefined_Error();
                break;
        }

        return isLoaded;
    }

    private protected async Task<bool> LoadDataAsync(string login, string password)
    {
        bool isLoaded;

        try
        {
            isLoaded = await LoadPassAsync(login, password);

            if (isLoaded) isLoaded = await LoadProfileAsync(Pass.Id);
        }
        catch
        {
            isLoaded = false;
        }

        return isLoaded;
    }

    public async Task RefreshView()
    {
        try
        {
            if (await CheckServerStatusAsync())
            {
                if (Preferences.ContainsKey("login") && Preferences.ContainsKey("password"))
                {
                    _login = Preferences.Get("login", null)!;
                    _password = Preferences.Get("password", null)!;

                    if (await LoadDataAsync(_login, _password))
                        IsAuthorized = true;
                    //await Shell.Current.GoToAsync($"//{nameof(FeedPage)}");
                    else
                        IsAuthorized = false;
                }
                else
                {
                    IsAuthorized = false;
                }
            }
            else
            {
                IsAuthorized = false;
            }
        }
        catch
        {
            SetUndefined_Error();
        }
        finally
        {
            IsBusy = false;
        }
    }
}