namespace KIBERmobile.HttpApi.JModels.JFeedbackForms;

public class JRequest
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

    public string feedbackid { get; set; }
    public string feedback_name { get; set; }
    public string feedback_phone { get; set; }
    public string feedback_email { get; set; }
    public string feedback_message { get; set; }

    public string manager_email { get; set; }
}