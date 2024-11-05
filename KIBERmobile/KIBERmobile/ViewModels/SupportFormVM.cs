using KIBERmobile.Models.FeedbackForms;
using static KIBERmobile.LocalPackages.DataProducer;

namespace KIBERmobile.ViewModels;

public class SupportFormVM : BaseViewModel
{
    private bool _isLoading;

    private bool _isSent;

    public bool IsSent
    {
        get => _isSent;
        private set => SetProperty(ref _isSent, value);
    }

    private ImageSource _formIcon;
    private string _formName;

    public ImageSource FormIcon
    {
        get => _formIcon;
        set => SetProperty(ref _formIcon, value);
    }

    public string FormName
    {
        get => _formName;
        set => SetProperty(ref _formName, value);
    }


    private string _formEntryNameLabel;
    private string _formEntryNamePlaceholder;
    private string? _formEntryNameValue;

    public string FormEntryNameLabel
    {
        get => _formEntryNameLabel;
        set => SetProperty(ref _formEntryNameLabel, value);
    }

    public string FormEntryNamePlaceholder
    {
        get => _formEntryNamePlaceholder;
        set => SetProperty(ref _formEntryNamePlaceholder, value);
    }

    public string? FormEntryNameValue
    {
        get => _formEntryNameValue;
        set => SetProperty(ref _formEntryNameValue, value);
    }


    private string _formEntrySecondNameLabel;
    private string _formEntrySecondNamePlaceholder;
    private string? _formEntrySecondNameValue;

    public string FormEntrySecondNameLabel
    {
        get => _formEntrySecondNameLabel;
        set => SetProperty(ref _formEntrySecondNameLabel, value);
    }

    public string FormEntrySecondNamePlaceholder
    {
        get => _formEntrySecondNamePlaceholder;
        set => SetProperty(ref _formEntrySecondNamePlaceholder, value);
    }

    public string? FormEntrySecondNameValue
    {
        get => _formEntrySecondNameValue;
        set => SetProperty(ref _formEntrySecondNameValue, value);
    }


    private string _formEntryPhoneLabel;
    private string _formEntryPhonePlaceholder;
    private string? _formEntryPhoneValue;

    public string FormEntryPhoneLabel
    {
        get => _formEntryPhoneLabel;
        set => SetProperty(ref _formEntryPhoneLabel, value);
    }

    public string FormEntryPhonePlaceholder
    {
        get => _formEntryPhonePlaceholder;
        set => SetProperty(ref _formEntryPhonePlaceholder, value);
    }

    public string? FormEntryPhoneValue
    {
        get => _formEntryPhoneValue;
        set => SetProperty(ref _formEntryPhoneValue, value);
    }

    private string _formEntryEmailLabel;
    private string _formEntryEmailPlaceholder;
    private string? _formEntryEmailValue;

    public string FormEntryEmailLabel
    {
        get => _formEntryEmailLabel;
        set => SetProperty(ref _formEntryEmailLabel, value);
    }

    public string FormEntryEmailPlaceholder
    {
        get => _formEntryEmailPlaceholder;
        set => SetProperty(ref _formEntryEmailPlaceholder, value);
    }

    public string? FormEntryEmailValue
    {
        get => _formEntryEmailValue;
        set => SetProperty(ref _formEntryEmailValue, value);
    }


    private string _formEditorMessageLabel;
    private string _formEditorMessagePlaceholder;
    private string? _formEditorMessageValue;

    public string FormEditorMessageLabel
    {
        get => _formEditorMessageLabel;
        set => SetProperty(ref _formEditorMessageLabel, value);
    }

    public string FormEditorMessagePlaceholder
    {
        get => _formEditorMessagePlaceholder;
        set => SetProperty(ref _formEditorMessagePlaceholder, value);
    }

    public string? FormEditorMessageValue
    {
        get => _formEditorMessageValue;
        set => SetProperty(ref _formEditorMessageValue, value);
    }

    public SupportFormVM(string formType)
    {
        RefreshViewCommand = new Command(() => RefreshView(formType));
        SendFormCommand = new Command(async () => await SendFormAsync(formType));
    }

    public Command RefreshViewCommand { get; }

    public Command SendFormCommand { get; }

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

    public async Task SendFormAsync(string formType)
    {
        _isLoading = true;
        IsBusy = true;

        SetStatusOK("Отправка сообщения...");

        switch (formType)
        {
            case "request":

                if (!IsNullOrEmpty(FormEntryNameValue, FormEntryPhoneValue, FormEditorMessageValue))
                {
                    if (!IsValidNumber(FormEntryPhoneValue))
                    {
                        SetInvalidPhoneNumber_Warning();
                    }
                    else
                    {
                        if (!IsNullOrEmpty(FormEntryEmailValue) && !IsValidEmail(FormEntryEmailValue))
                        {
                            SetInvalidEmail_Warning();
                        }
                        else
                        {
                            var requestForm = new Request()
                            {
                                ProfileId = SetProperty(Profile.ResidentId.ToString()),
                                ResidentParentFIO = SetProperty(Profile.ResidentParentsName),
                                ResidentName = SetProperty(Profile.ResidentFIO),
                                ResidentAge = SetResidentAge(Profile.ResidentBirthday),
                                ResidentProfileEmail = SetProperty(Profile.ResidentEmail),
                                ResidentParentPhone = SetProperty(Profile.ResidentParentsPhone),
                                CityName = SetProperty(Profile.CityName),
                                LocationName = SetProperty(Profile.LocationName),
                                GroupName = SetProperty(Profile.GroupName),
                                FeedbackId = "21704",
                                FeedbackName = SetProperty(FormEntryNameValue),
                                FeedbackPhone = SetProperty(FormEntryPhoneValue),
                                FeedbackEmail = SetProperty(FormEntryEmailValue),
                                FeedbackMessage = SetProperty(FormEditorMessageValue),
                                ManagerEmail = SetProperty(Profile.CityManagerEmail)
                            };

                            await FeedbackController.SendRequestFormAsync(requestForm);

                            switch (FeedbackController.Status)
                            {
                                case 200:
                                    SetStatusOK("Сообщение успешно отправлено!");
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

                break;

            case "callback":

                if (!IsNullOrEmpty(FormEntryNameValue, FormEntryPhoneValue))
                {
                    if (!IsValidNumber(FormEntryPhoneValue))
                    {
                        SetInvalidPhoneNumber_Warning();
                    }
                    else
                    {
                        var callbackForm = new Callback()
                        {
                            ProfileId = SetProperty(Profile.ResidentId.ToString()),
                            ResidentParentFIO = SetProperty(Profile.ResidentParentsName),
                            ResidentName = SetProperty(Profile.ResidentFIO),
                            ResidentAge = SetResidentAge(Profile.ResidentBirthday),
                            ResidentProfileEmail = SetProperty(Profile.ResidentEmail),
                            ResidentParentPhone = SetProperty(Profile.ResidentParentsPhone),
                            CityName = SetProperty(Profile.CityName),
                            LocationName = SetProperty(Profile.LocationName),
                            GroupName = SetProperty(Profile.GroupName),
                            FeedbackId = "21693",
                            CallbackName = SetProperty(FormEntryNameValue),
                            CallbackPhone = SetProperty(FormEntryPhoneValue),
                            ManagerEmail = SetProperty(Profile.CityManagerEmail)
                        };

                        await FeedbackController.SendCallbackFormAsync(callbackForm);

                        switch (FeedbackController.Status)
                        {
                            case 200:
                                SetStatusOK("Сообщение успешно отправлено!");
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
                else
                {
                    SetNullOrEmpty_Warning();
                }

                break;

            case "invite":

                // if (!IsNullOrEmpty(FormEntryNameValue, FormEntrySecondNameValue, FormEntryPhoneValue))
                // {
                //
                //     if (!IsValidNumber(FormEntryPhoneValue))
                //     {
                //         SetInvalidPhoneNumber_Warning();
                //     }
                //     else
                //     {
                //         if (!IsNullOrEmpty(FormEntryEmailValue) && !IsValidEmail(FormEntryEmailValue))
                //         {
                //             SetInvalidEmail_Warning();
                //         }
                //         else
                //         {
                //             var inviteForm = new Invite()
                //             {
                //                 ProfileId = SetProperty(Profile.ResidentId.ToString()),
                //                 ResidentParentFIO = SetProperty(Profile.ResidentParentsName),
                //                 ResidentName = SetProperty(Profile.ResidentFIO),
                //                 ResidentAge = SetResidentAge(Profile.ResidentBirthday),
                //                 ResidentProfileEmail = SetProperty(Profile.ResidentEmail),
                //                 ResidentParentPhone = SetProperty(Profile.ResidentParentsPhone),
                //                 CityName = SetProperty(Profile.CityName),
                //                 LocationName = SetProperty(Profile.LocationName),
                //                 GroupName = SetProperty(Profile.GroupName),
                //                 FriendParentFIO = SetProperty(FormEntryNameValue),
                //                 FriendResidentName = SetProperty(FormEntrySecondNameValue),
                //                 FriendEmail = SetProperty(FormEntryEmailValue),
                //                 FriendPhone = SetProperty(FormEntryPhoneValue),
                //                 ManagerEmail = SetProperty(Profile.CityManagerEmail)
                //             };
                //             
                //             await FeedbackController.SendInviteFormAsync(inviteForm);
                //         
                //             switch (FeedbackController.Status)
                //             {
                //                 case 200:
                //                     SetStatusOK("Сообщение успешно отправлено!");
                //                     IsSent = true;
                //                     break;
                //                 case -1:
                //                     SetLostConnect_Error();
                //                     break;
                //                 default:
                //                     SetUndefined_Error();
                //                     break;
                //             }
                //         }
                //     }
                // }
                // else
                // {
                //     SetNullOrEmpty_Warning();
                // }
                //
                break;

            case "tech":

                if (!IsNullOrEmpty(FormEntryEmailValue, FormEditorMessageValue))
                {
                    if (!IsValidEmail(FormEntryEmailValue))
                    {
                        SetInvalidEmail_Warning();
                    }
                    else
                    {
                        var tsrForm = new TechSupport()
                        {
                            ProfileId = SetProperty(Profile.ResidentId.ToString()),
                            ResidentParentFIO = SetProperty(Profile.ResidentParentsName),
                            ResidentName = SetProperty(Profile.ResidentFIO),
                            ResidentAge = SetResidentAge(Profile.ResidentBirthday),
                            ResidentProfileEmail = SetProperty(Profile.ResidentEmail),
                            ResidentParentPhone = SetProperty(Profile.ResidentParentsPhone),
                            CityName = SetProperty(Profile.CityName),
                            LocationName = SetProperty(Profile.LocationName),
                            GroupName = SetProperty(Profile.GroupName),
                            TsrEmail = SetProperty(FormEntryEmailValue),
                            TsrMessage = SetProperty(FormEditorMessageValue),
                            TsrDeviceInfo =
                                SetProperty("Функционал сбора информации об устройстве находится в разработке"),
                            ManagerEmail = SetProperty(SupportEmail)
                        };

                        await FeedbackController.SendTechSupportFormAsync(tsrForm);

                        switch (FeedbackController.Status)
                        {
                            case 200:
                                SetStatusOK("Сообщение успешно отправлено!");
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
                else
                {
                    SetNullOrEmpty_Warning();
                }

                break;
        }


        FormEntryNameValue = string.IsNullOrEmpty(_formEntryNameValue) ? string.Empty : _formEntryNameValue;
        FormEntrySecondNameValue =
            string.IsNullOrEmpty(_formEntrySecondNameValue) ? string.Empty : _formEntrySecondNameValue;
        FormEntryPhoneValue = string.IsNullOrEmpty(_formEntryPhoneValue) ? string.Empty : _formEntryPhoneValue;
        FormEntryEmailValue = string.IsNullOrEmpty(_formEntryEmailValue) ? string.Empty : _formEntryEmailValue;
        FormEditorMessageValue = string.IsNullOrEmpty(_formEditorMessageValue) ? string.Empty : _formEditorMessageValue;

        _isLoading = false;
        IsBusy = false;
    }

    private string? CheckValue(string? localValue, string? valueFromProfile)
    {
        return IsNullOrEmpty(localValue)
            ? IsNullOrEmpty(valueFromProfile)
                ? null
                : valueFromProfile
            : localValue;
    }

    private void RefreshView(string formType)
    {
        if (IsNullOrEmpty(FormEntryNameValue, FormEntryPhoneValue, FormEntryEmailValue, FormEditorMessageValue))
        {
            _ = IsConnected()
                ? SetStatusInfo("Пожалуйста, заполните все обязательные * поля ниже")
                : SetLostConnect_Error();

            switch (formType)
            {
                case "request":

                    FormIcon = "icon_about_module";
                    FormName = "Оставить сообщение";

                    FormEntryNameLabel = "Ваше имя *";
                    FormEntryNamePlaceholder = "Укажите ваше имя";
                    FormEntryNameValue = CheckValue(_formEntryNameValue, Profile.ResidentParentsName);

                    FormEntryPhoneLabel = "Телефон *";
                    FormEntryPhonePlaceholder = "Укажите ваш номер телефона";
                    FormEntryPhoneValue = CheckValue(_formEntryPhoneValue, Profile.ResidentParentsPhone);

                    FormEntryEmailLabel = "E-mail";
                    FormEntryEmailPlaceholder = "Укажите ваш e-mail адрес";
                    FormEntryEmailValue = CheckValue(_formEntryEmailValue, Profile.ResidentEmail);

                    FormEditorMessageLabel = "Текст сообщения *";
                    FormEditorMessagePlaceholder = "Введите текст вашего сообщения";
                    FormEditorMessageValue = CheckValue(_formEditorMessageValue, _formEditorMessageValue);

                    break;

                case "callback":

                    FormIcon = "icon_phone";
                    FormName = "Заказать обратный звонок";

                    FormEntryNameLabel = "Ваше имя *";
                    FormEntryNamePlaceholder = "Укажите ваше имя";
                    FormEntryNameValue = CheckValue(_formEntryNameValue, Profile.ResidentParentsName);

                    FormEntryPhoneLabel = "Телефон *";
                    FormEntryPhonePlaceholder = "Укажите ваш номер телефона";
                    FormEntryPhoneValue = CheckValue(_formEntryPhoneValue, Profile.ResidentParentsPhone);

                    break;

                case "invite":

                    // FormIcon = "icon_invite";
                    // FormName = "Пригласить друга";
                    //
                    // FormEntryNameLabel = "Имя родителя *";
                    // FormEntryNamePlaceholder = "Укажите ФИО родителя друга";
                    // FormEntryNameValue = null;
                    //
                    // FormEntrySecondNameLabel = "Имя ребёнка *";
                    // FormEntrySecondNamePlaceholder = "Укажите ФИО друга";
                    // FormEntrySecondNameValue = null;
                    //
                    // FormEntryPhoneLabel = "Телефон друга *";
                    // FormEntryPhonePlaceholder = "Укажите контактный номер друга";
                    // FormEntryPhoneValue = null;
                    //
                    // FormEntryEmailLabel = "E-mail друга";
                    // FormEntryEmailPlaceholder = "Укажите e-mail адрес друга";
                    // FormEntryEmailValue = null;

                    break;

                case "tech":

                    FormIcon = "icon_setting";
                    FormName = "Обращение в техподдержку";

                    FormEntryEmailLabel = "E-mail *";
                    FormEntryEmailPlaceholder = "Укажите ваш e-mail адрес";
                    FormEntryEmailValue = CheckValue(_formEntryEmailValue, Profile.ResidentEmail);

                    FormEditorMessageLabel = "Текст сообщения *";
                    FormEditorMessagePlaceholder = "Введите текст вашего сообщения";
                    FormEditorMessageValue = CheckValue(_formEditorMessageValue, _formEditorMessageValue);

                    break;
            }
        }
        else
        {
            FormEntryNameValue = string.IsNullOrEmpty(_formEntryNameValue) ? string.Empty : _formEntryNameValue;
            FormEntrySecondNameValue = string.IsNullOrEmpty(_formEntrySecondNameValue)
                ? string.Empty
                : _formEntrySecondNameValue;
            FormEntryPhoneValue = string.IsNullOrEmpty(_formEntryPhoneValue) ? string.Empty : _formEntryPhoneValue;
            FormEntryEmailValue = string.IsNullOrEmpty(_formEntryEmailValue) ? string.Empty : _formEntryEmailValue;
            FormEditorMessageValue =
                string.IsNullOrEmpty(_formEditorMessageValue) ? string.Empty : _formEditorMessageValue;
        }

        if (!_isLoading) IsBusy = false;
    }

    public void OnAppearing()
    {
        IsBusy = true;
    }
}