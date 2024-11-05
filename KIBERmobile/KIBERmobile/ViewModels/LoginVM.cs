using static KIBERmobile.LocalPackages.DataProducer;

namespace KIBERmobile.ViewModels;

public class LoginVM : StartVM
{
    private bool _isLoading = false;

    public LoginVM()
    {
        RefreshLoginViewCommand = new Command(RefreshView);

        ForgotPassCommand = new Command(() =>
            SetStatusWarning("Для восстановления пароля, пожалуйста, свяжитесь с вашим аккаунт-менеджером"));

        OpenPrivatePolicyCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.PrivacyPolicy));

        OpenClubTermsCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.ClubTerms));
    }

    public Command RefreshLoginViewCommand { get; }
    public Command ForgotPassCommand { get; }
    public Command OpenPrivatePolicyCommand { get; }
    public Command OpenClubTermsCommand { get; }

    public async Task OnLoginClickedAsync()
    {
        _isLoading = true;

        IsBusy = true;

        try
        {
            if (IsNullOrEmpty(Login, Password))
            {
                SetNullOrEmpty_Warning();
            }
            else
            {
                SetStatusInfo("Начало авторизации");

                if (await LoadDataAsync(Login, Password))
                {
                    Preferences.Set("login", Login);
                    Preferences.Set("password", Password);

                    IsAuthorized = true;
                }
            }

            Login = string.Empty;
            Password = string.Empty;
        }
        catch (Exception ex)
        {
            SetException_Error(ex);
        }
        finally
        {
            _isLoading = false;
            IsBusy = false;
        }
    }

    private new void RefreshView()
    {
        _ = IsConnected() ? SetStatusInfo("Введите логин и пароль") : SetLostConnect_Error();

        Login = IsNullOrEmpty(_login) ? string.Empty : _login;
        Password = IsNullOrEmpty(_password) ? string.Empty : _password;

        if (!_isLoading) IsBusy = false;
    }

    public void LoginVMOnAppearing()
    {
        IsBusy = true;
    }
}