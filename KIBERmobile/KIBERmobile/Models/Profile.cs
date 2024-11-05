namespace KIBERmobile.Models;

public class Profile
{
    //Resident 

    public int ResidentId { get; set; }
    public string? ResidentAvatarPath { get; set; }
    public string? ResidentFIO { get; set; }
    public int ResidentKiberonBalance { get; set; }
    public string? ResidentPayDay { get; set; }
    public string? ResidentBirthday { get; set; }
    public string? ResidentPhoneNumber { get; set; }
    public string? ResidentEmail { get; set; }
    public string? ResidentStartLearning { get; set; }
    public string? ResidentParentsName { get; set; }
    public string? ResidentParentsPhone { get; set; }
    public string? ResidentPortfolioLink { get; set; }

    //Group

    public int GroupId { get; set; }
    public string? GroupName { get; set; }


    public int LocationId { get; set; }
    public string? LocationName { get; set; }


    public int ModuleId { get; set; }
    public string? ModuleName { get; set; }
    public string? ModuleImage { get; set; }
    public string? ModuleVideoLink { get; set; }
    public int ModuleCurrentLesson { get; set; }
    public int ModuleCountLesson { get; set; }

    //City

    public int CityId { get; set; }
    public string? CityName { get; set; }
    public string? CityManagerPhoneNumber { get; set; }
    public string? CityManagerEmail { get; set; }
    public string? CityPaymentLink { get; set; }
    public string? CityKiberonChangeMessage { get; set; }
}