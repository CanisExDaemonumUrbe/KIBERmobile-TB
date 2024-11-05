using System;
using KIBERmobile.HttpApi;
using System.Threading.Tasks;

namespace KIBERmobile.Controllers;

public class ServerController
{
    private string _serverStatus;

    private readonly Connect _connect;

    public ref string ServerStatus => ref _serverStatus;

    public ServerController(Connect connect)
    {
        _connect = connect;
    }

    public async Task CheckServerStatusAsync()
    {
        _serverStatus = await _connect.CheckServerStatusAsync();
    }
}