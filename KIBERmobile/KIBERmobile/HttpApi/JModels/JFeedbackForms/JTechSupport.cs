namespace KIBERmobile.HttpApi.JModels.JFeedbackForms;

public class JTechSupport
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

    public string tsr_email { get; set; }
    public string tsr_message { get; set; }
    public string tsr_device_info { get; set; }

    public string manager_email { get; set; }
}