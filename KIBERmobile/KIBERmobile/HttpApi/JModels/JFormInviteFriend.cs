using System;
using System.Collections.Generic;
using System.Text;

namespace KIBERmobile.HttpApi.JModels;

public class JFormInviteFriend
{
    public string user_name { get; set; }
    public int? id { get; set; }
    public int user_id { get; set; }
    public string to_email { get; set; }
    public string to_name { get; set; }
}