using static KIBERmobile.LocalPackages.DataProducer;

namespace KIBERmobile.ViewModels;

internal class ChangePasswordVM : BaseViewModel
{
    private bool _isLoading;

    private bool _isSent;

    public bool IsSent
    {
        get => _isSent;
        set => SetProperty(ref _isSent, value);
    }


    private string _oldPassword;
    private string _newPassword;
    private string _copyNewPassword;

    public string OldPassword
    {
        get => _oldPassword;
        set => SetProperty(ref _oldPassword, value);
    }

    public string NewPassword
    {
        get => _newPassword;
        set => SetProperty(ref _newPassword, value);
    }

    public string CopyNewPassword
    {
        get => _copyNewPassword;
        set => SetProperty(ref _copyNewPassword, value);
    }


    public ChangePasswordVM()
    {
        RefreshViewCommand = new Command(() => RefreshView());
        ForgotPassCommand = new Command(() =>
            SetStatusWarning("Для восстановления пароля, пожалуйста, свяжитесь с вашим аккаунт-менеджером"));
    }

    public Command RefreshViewCommand { get; }
    public Command ForgotPassCommand { get; }

    public async Task SaveChangedPasswordAsync()
    {
        _isLoading = false;
        IsBusy = true;

        if (!IsNullOrEmpty(OldPassword, NewPassword, CopyNewPassword))
        {
            if (OldPassword != AuthorizationController.Pass.Password)
            {
                SetStatusWarning("Неверный старый пароль!");
            }
            else
            {
                if (NewPassword != CopyNewPassword)
                {
                    SetStatusWarning("Новые пароли не совпадают!");
                }
                else
                {
                    SetStatusOK("Сохранение...");

                    await AuthorizationController.ChangeUserPasswordAsync(NewPassword);

                    switch (AuthorizationController.Status)
                    {
                        case 200:
                            Preferences.Remove("password");
                            Preferences.Set("password", NewPassword);

                            SetStatusOK("Пароль успешно изменён!");
                            IsSent = true;
                            break;
                        case -1:
                            SetLostConnect_Error();
                            break;
                        default:
                            SetUndefined_Error();
                            break;
                    }
                }
            }
        }
        else
        {
            SetNullOrEmpty_Warning();
        }

        OldPassword = string.Empty;
        NewPassword = string.Empty;
        CopyNewPassword = string.Empty;

        _isLoading = false;
        IsBusy = false;
    }

    private void RefreshView()
    {
        if (IsNullOrEmpty(_oldPassword, _newPassword, _copyNewPassword))
        {
            _ = IsConnected() ? SetStatusInfo("Подтвердите старый пароль и укажите новый") : SetLostConnect_Error();
            IsBusy = false;
        }
        else
        {
            OldPassword = string.IsNullOrEmpty(_oldPassword) ? string.Empty : OldPassword;
            NewPassword = string.IsNullOrEmpty(_newPassword) ? string.Empty : NewPassword;
            CopyNewPassword = string.IsNullOrEmpty(_copyNewPassword) ? string.Empty : CopyNewPassword;
        }

        if (!_isLoading) IsBusy = false;
    }

    public void OnAppearing()
    {
        IsBusy = true;
    }
}