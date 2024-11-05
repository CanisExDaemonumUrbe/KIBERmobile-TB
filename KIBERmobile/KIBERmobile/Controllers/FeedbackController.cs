using KIBERmobile.HttpApi;
using KIBERmobile.Models.FeedbackForms;

namespace KIBERmobile.Controllers;

/// <summary>
/// Ver: 0.1
/// </summary>
public class FeedbackController(Connect connect)
{
    private int _status;

    public ref int Status => ref _status;

    public async Task SendInviteFormAsync(Invite inviteForm)
    {
        var responseResult = await connect.SendInviteFormAsync(inviteForm);
        _status = responseResult;
    }

    public async Task SendCallbackFormAsync(Callback callbackForm)
    {
        var responseResult = await connect.SendCallbackFormAsync(callbackForm);
        _status = responseResult;
    }

    public async Task SendRequestFormAsync(Request requestForm)
    {
        var responseResult = await connect.SendFeedbackFormAsync(requestForm);
        _status = responseResult;
    }

    public async Task SendTechSupportFormAsync(TechSupport tsForm)
    {
        var responseResult = await connect.SendTechSupportFormAsync(tsForm);
        _status = responseResult;
    }
}