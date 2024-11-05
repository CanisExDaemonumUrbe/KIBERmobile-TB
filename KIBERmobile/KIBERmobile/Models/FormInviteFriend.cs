using System;
using System.Collections.Generic;
using System.Text;

namespace KIBERmobile.Models;

public class FormInviteFriend
{
    public string UserName { get; set; }
    public int? Id { get; set; }
    public int UserId { get; set; }
    public string ToEmail { get; set; }
    public string ToName { get; set; }
}