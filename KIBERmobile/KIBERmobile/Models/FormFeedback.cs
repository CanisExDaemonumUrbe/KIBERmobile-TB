using System;
using System.Collections.Generic;
using System.Text;

namespace KIBERmobile.Models;

public class FormFeedback
{
    public string ManagerEmail { get; set; }
    public int? Id { get; set; }
    public int FeedbackId { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string UserPhone { get; set; }
    public string Message { get; set; }
}