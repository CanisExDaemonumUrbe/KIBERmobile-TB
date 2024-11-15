using System.Runtime.CompilerServices;
using KIBERmobile.Models;
using KIBERmobile.Models.FeedbackForms;
using KIBERmobile.HttpApi.JModels;
using KIBERmobile.HttpApi.JModels.JFeedbackForms;


namespace KIBERmobile.HttpApi;

public static class Mapper
{
    private static string TryFormatDate(string date)
    {
        try
        {
            return string.IsNullOrEmpty(date) ? "Не указано" : DateTime.Parse(date).ToString("dd.MM.yyyy");
        }
        catch
        {
            return DateTime.Now.ToString("dd.MM.yyyy");
        }
    }

    private static int TryIntParse(string value)
    {
        try
        {
            return string.IsNullOrEmpty(value) ? -1 : int.Parse(value);
        }
        catch
        {
            return -1;
        }
    }

    private static string TryGetImageLink(string gateUrl, string imageType, string imageName)
    {
        var link = $"{gateUrl}/static/image";

        try
        {
            link = $"{link}/{imageType}/{imageName}";
        }
        catch
        {
            link = $"{link}/image/user/error/error";
        }

        return link;
    }

    public static Pass JPassToPass(JPass jPass)
    {
        var pass = new Pass()
        {
            Id = jPass.id,
            Password = jPass.password
        };

        return pass;
    }

    public static JPass PassToJPass(Pass pass)
    {
        var jPass = new JPass()
        {
            id = pass.Id,
            password = pass.Password
        };

        return jPass;
    }

    public static JRegistration RegistrationToJRegistration(Registration form)
    {
        return new JRegistration()
        {
            city_name = form.CityName,
            fio = form.Fio,
            email = form.Email,
            phone_number = form.PhoneNumber,
            resident_age = form.ResidentAge,
            manager_email = form.ManagerEmail
        };
    }

    public static Profile JProfileToProfile(JProfile jProfile, string gateImageUrl)
    {
        var profile = new Profile()
        {
            ResidentId = TryIntParse(jProfile.residentId),
            ResidentAvatarPath = TryGetImageLink(gateImageUrl, "user", jProfile.residentImage),
            ResidentFIO = jProfile.residentFIO,
            ResidentKiberonBalance = TryIntParse(jProfile.residentKiberoneBalance),
            ResidentPayDay = jProfile.residentPayDay,
            ResidentBirthday = jProfile.residentBirthday,
            ResidentPhoneNumber = jProfile.residentPhoneNumber,
            ResidentEmail = jProfile.residentEmail,
            ResidentStartLearning = jProfile.residentStartLearning,
            ResidentParentsName = jProfile.residentParentsName,
            ResidentParentsPhone = jProfile.residentParentsPhone,
            ResidentPortfolioLink = jProfile.residentPorfolioLink,

            GroupId = TryIntParse(jProfile.groupId),
            GroupName = jProfile.groupName,

            LocationId = TryIntParse(jProfile.locationId),
            LocationName = jProfile.locationName,

            ModuleId = TryIntParse(jProfile.moduleId),
            ModuleName = jProfile.moduleName,
            ModuleImage = TryGetImageLink(gateImageUrl, "module", jProfile.moduleImage),
            ModuleVideoLink = jProfile.moduleVideo,
            ModuleCurrentLesson = TryIntParse(jProfile.sheduleCurrentLesson),
            ModuleCountLesson = TryIntParse(jProfile.moduleCountLesson),

            CityId = TryIntParse(jProfile.cityId),
            CityName = jProfile.cityName,
            CityManagerPhoneNumber = jProfile.cityManagerPhone,
            CityManagerEmail = jProfile.cityManagerEmail,
            CityPaymentLink = jProfile.cityPaymentLink,
            CityKiberonChangeMessage = jProfile.cityKiberonChangeMessage
        };

        return profile;
    }

    public static Item JItemToItem(JItem jItem, string gateImageUrl)
    {
        var baseImgUrl = TryGetImageLink(gateImageUrl, "kibershop-item", jItem.image);

        var item = new Item()
        {
            Id = jItem.id,
            CityId = jItem.city_id,
            Active = jItem.active,
            Title = jItem.title,
            Description = jItem.description,
            BigImages = baseImgUrl + "/big",
            IconImages = baseImgUrl + "/icon",
            Price = jItem.price
        };

        return item;
    }

    public static News JNewsToNews(JNews jNews, string gateImageUrl)
    {
        var baseImgUrl = TryGetImageLink(gateImageUrl, "city-news", jNews.image);
        
        var news = new News()
        {
            Id = jNews.id,
            CityId = jNews.city_id,
            Type = CheckType(jNews.section_id),
            Active = jNews.active,
            Title = jNews.title,
            Anons = jNews.anons,
            Description = jNews.description,
            BigImages = baseImgUrl + "/big",
            IconImages = baseImgUrl + "/icon",
            Date = TryFormatDate(jNews.date)
        };
        return news;

        static string CheckType(int sectionId)
        {
            var remainder = sectionId % 1000;
            return remainder switch
            {
                697 => "icon_type_stock",
                698 => "icon_type_events",
                _ => "icon_type_news"
            };
        }
    }

    public static KiberonLog JKiberonLogToKiberonLog(JKiberonLog jKiberonLog)
    {
        var log = new KiberonLog()
        {
            Id = jKiberonLog.id,
            Date = TryFormatDate(jKiberonLog.date),
            Sign = jKiberonLog.sign > 0 ? "+" : "-",
            Color = jKiberonLog.sign > 0 ? Colors.Green : Colors.Red,
            Amount = jKiberonLog.amount,
            Balance = jKiberonLog.balance,
            Comment = jKiberonLog.comment
        };
        return log;
    }

    public static JInvite InviteFormToJInviteForm(Invite form)
    {
        return new JInvite()
        {
            id = null,
            profile_id = form.ProfileId,
            resident_parent_fio = form.ResidentParentFIO,
            resident_name = form.ResidentName,
            resident_age = form.ResidentAge,
            resident_profile_email = form.ResidentProfileEmail,
            resident_parent_phone = form.ResidentParentPhone,
            city_name = form.CityName,
            location_name = form.LocationName,
            group_name = form.GroupName,
            friend_parent_fio = form.FriendParentFIO,
            friend_resident_name = form.FriendResidentName,
            friend_email = form.FriendEmail,
            friend_phone = form.FriendPhone,
            manager_email = form.ManagerEmail
        };
    }

    public static JCallback CallbackFormToJCallbackForm(Callback form)
    {
        return new JCallback()
        {
            id = null,
            profile_id = form.ProfileId,
            resident_parent_fio = form.ResidentParentFIO,
            resident_name = form.ResidentName,
            resident_age = form.ResidentAge,
            resident_profile_email = form.ResidentProfileEmail,
            resident_parent_phone = form.ResidentParentPhone,
            city_name = form.CityName,
            location_name = form.LocationName,
            group_name = form.GroupName,
            feedbackid = form.FeedbackId,
            callback_name = form.CallbackName,
            callback_phone = form.CallbackPhone,
            manager_email = form.ManagerEmail
        };
    }

    public static JRequest RequestFormToJRequestForm(Request form)
    {
        return new JRequest()
        {
            id = null,
            profile_id = form.ProfileId,
            resident_parent_fio = form.ResidentParentFIO,
            resident_name = form.ResidentName,
            resident_age = form.ResidentAge,
            resident_profile_email = form.ResidentProfileEmail,
            resident_parent_phone = form.ResidentParentPhone,
            city_name = form.CityName,
            location_name = form.LocationName,
            group_name = form.GroupName,
            feedbackid = form.FeedbackId,
            feedback_name = form.FeedbackName,
            feedback_phone = form.FeedbackPhone,
            feedback_email = form.FeedbackEmail,
            feedback_message = form.FeedbackMessage,
            manager_email = form.ManagerEmail
        };
    }

    public static JTechSupport TechSupportFormToJTechSupportForm(TechSupport form)
    {
        return new JTechSupport()
        {
            id = null,
            profile_id = form.ProfileId,
            resident_parent_fio = form.ResidentParentFIO,
            resident_name = form.ResidentName,
            resident_age = form.ResidentAge,
            resident_profile_email = form.ResidentProfileEmail,
            resident_parent_phone = form.ResidentParentPhone,
            city_name = form.CityName,
            location_name = form.LocationName,
            group_name = form.GroupName,
            tsr_email = form.TsrEmail,
            tsr_message = form.TsrMessage,
            tsr_device_info = form.TsrDeviceInfo,
            manager_email = form.ManagerEmail
        };
    }
}