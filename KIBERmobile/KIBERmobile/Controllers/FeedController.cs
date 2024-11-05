using KIBERmobile.HttpApi;
using KIBERmobile.HttpApi.JModels;
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
public class FeedController(Connect connect)
{
    private static ObservableCollection<News> _news = [];

    private int _status = 0;

    public ref ObservableCollection<News> Collection => ref _news;

    public ref int Status => ref _status;

    public async Task LoadDataAsync(int cityId)
    {
        if (_status != 200)
        {
            var (newsCollection, status) = await connect.LoadCityFeedDataAsync(cityId);

            switch (status)
            {
                case < 0:
                    newsCollection.Add(new News()
                    {
                        Id = 1,
                        CityId = -1,
                        Type = "icon_type_lost_connect",
                        Active = -1,
                        Title = "Ошибка сети!",
                        Anons = "Устройство не подключено к интернету!",
                        Description = "Восстановите связь и попробуйте снова",
                        IconImages = "image_lost_connection_small",
                        BigImages = "image_lost_connection_big",
                        Date = DateTime.Now.ToShortDateString()
                    });
                    break;
                case 404 when newsCollection.Count == 0:
                    newsCollection.Add(new News()
                    {
                        Id = 1,
                        CityId = -1,
                        Type = "icon_type_not_found",
                        Active = 404,
                        Title = "Новостей пока нет!",
                        Anons = "",
                        Description = "",
                        IconImages = "image_no_news_small",
                        BigImages = "image_no_news_big",
                        Date = DateTime.Now.ToShortDateString()
                    });
                    break;
            }

            _news = newsCollection;
            _status = status;
        }
    }


    public async Task<News?> GetItemAsync(int cityId)
    {
        return await Task.FromResult(_news.FirstOrDefault(s => s.Id == cityId));
    }

    public void Clear()
    {
        _status = 0;
        _news = [];
    }
}