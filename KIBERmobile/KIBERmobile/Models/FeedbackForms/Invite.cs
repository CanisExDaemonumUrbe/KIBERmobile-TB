namespace KIBERmobile.Models.FeedbackForms;

public class Invite
{
    public string ProfileId { get; set; }

    public string ResidentParentFIO { get; set; }
    public string ResidentName { get; set; }
    public string ResidentAge { get; set; }

    public string ResidentProfileEmail { get; set; }

    public string ResidentParentPhone { get; set; }

    public string CityName { get; set; }
    public string LocationName { get; set; }
    public string GroupName { get; set; }


    public string FriendParentFIO { get; set; }
    public string FriendResidentName { get; set; }
    public string FriendEmail { get; set; }
    public string FriendPhone { get; set; }

    public string ManagerEmail { get; set; }
}