using KIBERmobile.Controllers;
using KIBERmobile.HttpApi;
using KIBERmobile.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace KIBERmobile.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    public readonly DeviceIdiom DeviceIdiom = DeviceInfo.Current.Idiom;
    public readonly DevicePlatform DevicePlatform = DeviceInfo.Current.Platform;
    public readonly double DisplayWidth = DeviceDisplay.Current.MainDisplayInfo.Width;
    public readonly double DisplayHeight = DeviceDisplay.Current.MainDisplayInfo.Height;
    public readonly double DisplayDensity = DeviceDisplay.MainDisplayInfo.Density;
    public readonly DisplayOrientation DeviceOrientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;


    private string _supportEmail = "app@kiber1.com";

    public string SupportEmail
    {
        get => _supportEmail;
        set => SetProperty(ref _supportEmail, value);
    }

    public string AppVersion => GetAppVersion();

    private string GetAppVersion()
    {
        var baseVersion = "1.4.7:";

        const string androidVersion = "A-0";
        const string iosVersion = "I-0";

        if (DevicePlatform == DevicePlatform.iOS) baseVersion += iosVersion;
        if (DevicePlatform == DevicePlatform.Android) baseVersion += androidVersion;

        if (DeviceIdiom == DeviceIdiom.Phone) baseVersion += "|P";

        if (DeviceIdiom == DeviceIdiom.Tablet) baseVersion += "|T";

        return baseVersion;
    }

    private readonly string _pageTitle = "KIBERone";

    public string PageTitle
    {
        get => _pageTitle;
        protected init => SetProperty(ref _pageTitle, value);
    }


    private static readonly Connect Connect = new();

    private bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    protected static readonly ServerController ServerController = new(Connect);
    public string ServerStatus => ServerController.ServerStatus;

    protected static readonly AuthorizationController AuthorizationController = new(Connect);
    protected static Pass Pass => AuthorizationController.Pass;

    protected static readonly ProfileController ProfileController = new(Connect);
    public Profile Profile => ProfileController.Profile;

    protected static readonly ShopController ShopController = new(Connect);

    protected static readonly FeedController FeedController = new(Connect);

    protected static readonly LogsController LogsController = new(Connect);

    protected static readonly FeedbackController FeedbackController = new(Connect);

    #region InternetChekMethod

    protected bool IsConnected()
    {
        return Connectivity.NetworkAccess == NetworkAccess.Internet;
    }

    #endregion


    protected async Task<bool> OpenLinkAsync(string link)
    {
        IsBusy = true;

        try
        {
            await Browser.OpenAsync(link, BrowserLaunchMode.SystemPreferred);
            return true;
        }
        catch (Exception err)
        {
            var log = err.Message;

            return false;
        }
        finally
        {
            IsBusy = false;
        }
    }

    #region DocumentsLinks

    protected static class DocumentsLink
    {
        private const string _root = "https://kiber-one.fun/gate/v1/static";

        public static readonly string PrivacyPolicy = $"{_root}/privacy-policy.pdf";
        public static readonly string ClubTerms = $"{_root}/club-terms.pdf";
        public static readonly string FeedTerms = $"{_root}/feed-terms.pdf";
        public static readonly string ShopTerms = $"{_root}/shop-terms.pdf";
    }

    #endregion

    #region StatusMessage

    private string? _statusMessage;
    private Color? _statusColor;

    public string? StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public Color? StatusColor
    {
        get => _statusColor;
        set => SetProperty(ref _statusColor, value);
    }

    protected bool SetLostConnect_Error()
    {
        return SetStatusError("Отсутствует подключение к интернету!");
    }

    protected bool SetUndefined_Error()
    {
        return SetStatusError("OOPS! что-то пошло не так...");
    }

    protected bool SetException_Error(Exception ex)
    {
        return SetStatusError(ex.Message);
    }

    protected bool SetNullOrEmpty_Warning()
    {
        return SetStatusWarning("Все обязательные поля должны быть заполнены!");
    }

    protected bool SetInvalidEmail_Warning()
    {
        return SetStatusWarning("Некорректный Email!");
    }

    protected bool SetInvalidPhoneNumber_Warning()
    {
        return SetStatusWarning("Некорректный номер телефона!");
    }

    protected bool SetStatusInfo(string? message)
    {
        try
        {
            StatusMessage = message;
            StatusColor = StatusColors.Info;
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected bool SetStatusOK(string? message)
    {
        try
        {
            StatusMessage = message;
            StatusColor = StatusColors.OK;
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected bool SetStatusWarning(string? message)
    {
        try
        {
            StatusMessage = message;
            StatusColor = StatusColors.Warning;
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected bool SetStatusError(string? message)
    {
        try
        {
            StatusMessage = message;
            StatusColor = StatusColors.Error;
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected static class StatusColors
    {
        public static Color? Info => Color.FromArgb("#FFFFFF");
        public static Color? Error => Colors.Red;
        public static Color? Warning => Color.FromArgb("#00C8C8");
        public static Color? OK => Color.FromArgb("#F7C90E");
    }

    #endregion

    #region INotifyPropertyChanged

    protected bool SetProperty<T>(ref T backingStore, T value,
        [CallerMemberName] string propertyName = "",
        Action? onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;

        changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}