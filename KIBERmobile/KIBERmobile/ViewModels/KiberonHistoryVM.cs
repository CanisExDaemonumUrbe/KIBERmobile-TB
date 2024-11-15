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
    private bool _isLoaded;
    
    private ref ObservableCollection<KiberonLog> _logs => ref LogsController.Collection;
    public ObservableCollection<KiberonLog> Logs { get; set; }

    private static ObservableCollection<KiberonLog> _cashedLogs = [];

    public KiberonHistoryVM()
    {
        PageTitle = "Баланс киберонов: ";
        RefreshViewCommand = new Command(async () => await RefreshViewAsync(Profile.ResidentId));

        Logs = _cashedLogs.Count == 0 ? [] : _cashedLogs;
    }

    public Command RefreshViewCommand { get; }

    private async Task RefreshViewAsync(int userId)
    {
        
        if (LogsController.Status != 200)
        {
            Logs.Clear();
            
            await LogsController.LoadDataAsync(userId);

            for (var i = _logs.Count - 1; i >= 0; i--) Logs.Add(_logs[i]);

            LogsController.Status = 200;
            _cashedLogs = Logs;

            _isLoaded = true;
        }

        IsBusy = false;
    }

    public void OnAppearing()
    {
        if (!_isLoaded) IsBusy = true;
    }
}