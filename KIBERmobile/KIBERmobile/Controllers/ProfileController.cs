using KIBERmobile.Models;
using KIBERmobile.HttpApi;

namespace KIBERmobile.Controllers;

/// <summary>
/// Ver: 0.2 - Load if empty
/// </summary>
public class ProfileController(Connect connect)
{
    private Profile _profile;

    private int _status = 0;

    public ref Profile Profile => ref _profile;

    public ref int Status => ref _status;

    public async Task LoadProfileDataAsync(int id)
    {
        if (_status != 200)
        {
            var (profile, status) = await connect.LoadProfileDataAsync(id);
            _profile = profile;
            _status = status;
        }
    }

    public void Clear()
    {
        _status = 0;
        _profile = null;
    }
}