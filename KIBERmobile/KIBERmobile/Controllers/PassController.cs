using KIBERmobile.HttpApi;
using KIBERmobile.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KIBERmobile.Controllers;

/// <summary>
/// Ver: 0.2 - Load if empty
/// </summary>
public class PassController(Connect connect)
{
    private Pass _pass = new();

    private int _status;

    public ref Pass Pass => ref _pass;

    public ref int Status => ref _status;

    public async Task GetPassAsync(string login, string password)
    {
        if (_status != 200)
        {
            var (pass, status) = await connect.LoginUserAsync(login, password);
            _pass = pass;
            _status = status;
        }
    }


    public async Task ChangeUserPasswordAsync(string newPassword)
    {
        var responseResult = await connect.ChangeUserPasswordAsync(_pass, newPassword);
        if (responseResult == 200)
            _pass.Password = newPassword;
        else
            _status = responseResult;
    }


    public void Clear()
    {
        _status = 0;
        _pass = new Pass();
    }
}