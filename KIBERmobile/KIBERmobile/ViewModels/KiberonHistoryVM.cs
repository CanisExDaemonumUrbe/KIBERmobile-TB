using KIBERmobile.Models;
using KIBERmobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace KIBERmobile.ViewModels;

internal class KiberonHistoryVM : BaseViewModel
{
    private ref ObservableCollection<KiberonLog> _logs => ref LogsController.Collection;

    private static ObservableCollection<KiberonLog>? _cashedCollection;
    public ObservableCollection<KiberonLog> Logs { get; }

    public KiberonHistoryVM()
    {
        PageTitle = "Баланс киберонов: ";

        RefreshViewCommand = new Command(async () => await RefreshViewAsync(Profile.ResidentId));

        Logs = _cashedCollection ?? [];
    }

    public Command RefreshViewCommand { get; }

    private async Task RefreshViewAsync(int userId)
    {
        if (LogsController.Status != 200)
        {
            Logs.Clear();
            
            await LogsController.LoadDataAsync(userId);

            await Task.Run(() =>
            {
                for (var i = _logs.Count - 1; i >= 0; i--) Logs.Add(_logs[i]);
            });

            _cashedCollection = Logs;

            LogsController.Status = 200;
        }

        IsBusy = false;
    }

    public void OnAppearing()
    {
        IsBusy = true;
    }
}