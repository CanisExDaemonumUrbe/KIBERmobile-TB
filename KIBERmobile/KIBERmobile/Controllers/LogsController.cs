using KIBERmobile.HttpApi;
using KIBERmobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Maui.Graphics.Color;

namespace KIBERmobile.Controllers;

/// <summary>
/// Ver: 0.2 - Load if empty
/// </summary>
public class LogsController(Connect connect)
{
    private static ObservableCollection<KiberonLog> _logs = [];

    private int _status = 0;

    public ref ObservableCollection<KiberonLog> Collection => ref _logs;
    public ref int Status => ref _status;

    public async Task LoadDataAsync(int userId)
    {
        if (_status != 200)
        {
            var (logsCollection, status) =
                await connect.LoadKiberonLogsDataStoreAsync(userId);

            switch (status)
            {
                case < 0:
                    logsCollection.Add(new KiberonLog()
                    {
                        Id = 1,
                        Date = DateTime.Now.ToShortDateString(),
                        Sign = "!",
                        Color = Color.FromArgb("#92241F"),
                        Amount = 0,
                        Balance = 0,
                        Comment = "Устройство не подключено к интернету! Восстановите связь и попробуйте снова"
                    });
                    break;
                case 404 when logsCollection.Count == 0:
                    logsCollection.Add(new KiberonLog()
                    {
                        Id = 1,
                        Date = DateTime.Now.ToShortDateString(),
                        Sign = "~",
                        Color = Color.FromArgb("#0E4963"),
                        Amount = 0,
                        Balance = 0,
                        Comment = "Начни посещать занятия и получай свои кибероны прямо сейчас!"
                    });
                    break;
            }

            _logs = logsCollection;
            _status = status;
        }
    }

    public void Clear()
    {
        _status = 0;
        _logs = [];
    }
}