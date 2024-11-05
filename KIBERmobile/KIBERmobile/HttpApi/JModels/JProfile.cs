namespace KIBERmobile.HttpApi.JModels;

public class JProfile
{
    public string? residentId { get; set; }
    public string? residentImage { get; set; }
    public string? residentFIO { get; set; }
    public string? residentKiberoneBalance { get; set; }
    public string? residentPayDay { get; set; }
    public string? residentBirthday { get; set; }
    public string? residentPhoneNumber { get; set; }
    public string? residentEmail { get; set; }
    public string? residentStartLearning { get; set; }
    public string? residentParentsName { get; set; }
    public string? residentParentsPhone { get; set; }

    public string? residentPorfolioLink { get; set; }

    public string? groupId { get; set; }
    public string? groupName { get; set; }


    public string? locationId { get; set; }
    public string? locationName { get; set; }


    public string? sheduleCurrentLesson { get; set; }


    public string? moduleId { get; set; }
    public string? moduleName { get; set; }
    public string? moduleImage { get; set; }
    public string? moduleVideo { get; set; }
    public string? moduleCountLesson { get; set; }


    public string? cityId { get; set; }
    public string? cityName { get; set; }
    public string? cityManagerPhone { get; set; }

    public string? cityManagerEmail { get; set; }
    public string? cityPaymentLink { get; set; }
    public string? cityKiberonChangeMessage { get; set; }
}