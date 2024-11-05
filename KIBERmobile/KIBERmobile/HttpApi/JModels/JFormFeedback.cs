using System;
using System.Collections.Generic;
using System.Text;

namespace KIBERmobile.HttpApi.JModels;

public class JFormFeedback
{
    public string manager_email { get; set; }
    public int? id { get; set; }
    public int feedbackid { get; set; }
    public string ip { get; set; }
    public string email { get; set; }
    public string user_name { get; set; }
    public string user_phone { get; set; }
    public string field_message { get; set; }
}