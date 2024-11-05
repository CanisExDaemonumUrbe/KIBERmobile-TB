using KIBERmobile.HttpApi;
using KIBERmobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIBERmobile.Controllers;

/// <summary>
/// Ver: 0.2 - Load if empty
/// </summary>
public class ShopController(Connect connect)
{
    private static ObservableCollection<Item> _items = [];

    private int _status = 0;

    public ref ObservableCollection<Item> Collection => ref _items;

    public ref int Status => ref _status;

    public async Task LoadDataAsync(int cityId)
    {
        if (_status != 200)
        {
            var (itemsCollection, status) = await connect.LoadCityShopDataAsync(cityId);

            switch (status)
            {
                case < 0:
                    itemsCollection.Add(new Item()
                    {
                        Id = 1,
                        CityId = -1,
                        Active = -1,
                        Title = "Ошибка сети!",
                        Description = "Устройство не подключено к интернету! Восстановите связь и попробуйте снова",
                        IconImages = "image_lost_connection_shop",
                        BigImages = "image_lost_connection_shop",
                        Price = 0
                    });
                    break;
                case 404 when itemsCollection.Count == 0:
                    itemsCollection.Add(new Item()
                    {
                        Id = 1,
                        CityId = -1,
                        Active = 404,
                        Title = "Магазин пуст!",
                        Description = "Пока что тут ничего нет, но скоро мы обязательно что-нибудь добавим!",
                        IconImages = "image_no_items_shop",
                        BigImages = "image_no_items_shop",
                        Price = 0
                    });
                    break;
            }

            _items = itemsCollection;
            _status = status;
        }
    }

    public async Task<Item?> GetItemAsync(int id)
    {
        return await Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
    }

    public void Clear()
    {
        _status = 0;
        _items = [];
    }
}