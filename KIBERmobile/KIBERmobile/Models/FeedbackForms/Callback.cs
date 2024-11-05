namespace KIBERmobile.Models.FeedbackForms;

public class Callback
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

    public string FeedbackId { get; set; }

    public string CallbackName { get; set; }
    public string CallbackPhone { get; set; }

    public string ManagerEmail { get; set; }
}