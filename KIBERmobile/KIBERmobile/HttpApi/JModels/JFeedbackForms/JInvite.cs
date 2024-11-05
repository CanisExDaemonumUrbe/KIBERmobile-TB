namespace KIBERmobile.HttpApi.JModels.JFeedbackForms;

public class JInvite
{
    public string? id { get; set; }
    public string profile_id { get; set; }

    public string resident_parent_fio { get; set; }
    public string resident_name { get; set; }
    public string resident_age { get; set; }

    public string resident_profile_email { get; set; }

    public string resident_parent_phone { get; set; }

    public string city_name { get; set; }
    public string location_name { get; set; }
    public string group_name { get; set; }

    public string friend_parent_fio { get; set; }
    public string friend_resident_name { get; set; }
    public string friend_email { get; set; }
    public string friend_phone { get; set; }

    public string manager_email { get; set; }
}