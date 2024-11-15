using KIBERmobile.Models;
using KIBERmobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace KIBERmobile.ViewModels;

public class KIBERshopVM : BaseViewModel
{
    private bool _isLoaded;
    private int _balance;
    
    public int Balance
    {
        get => _balance;
        set => SetProperty(ref _balance, value);
    }

    private ref ObservableCollection<Item> _items => ref ShopController.Collection;
    public ObservableCollection<Item> Items { get; }

    public KIBERshopVM()
    {
        PageTitle = "Баланс киберонов: ";
        Balance = Profile.ResidentKiberonBalance;

        RefreshViewCommand = new Command(async () => await RefreshViewAsync(Profile.CityId));
        Items = new ObservableCollection<Item>();
        OpenShopTermsCommand = new Command(async () =>
            await OpenLinkAsync(DocumentsLink.ShopTerms));
    }

    public Command OpenShopTermsCommand { get; }
    public Command RefreshViewCommand { get; }
    
    private async Task RefreshViewAsync(int cityId)
    {

        if (ShopController.Status != 200)
        {
            Items.Clear();

            await ShopController.LoadDataAsync(cityId);

            for (var i = _items.Count - 1; i >= 0; i--) Items.Add(_items[i]);

            ShopController.Status = 200;

            _isLoaded = true;
        }

        IsBusy = false;
    }


    public void OnAppearing()
    {
        if (!_isLoaded) IsBusy = true;
    }
}