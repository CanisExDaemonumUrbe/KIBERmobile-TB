using KIBERmobile.HttpApi.JModels;
using KIBERmobile.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.ObjectModel;
using KIBERmobile.Models.FeedbackForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KIBERmobile.HttpApi;

//Ver 1.*.*
public class Connect
{
    private static readonly HttpClient HttpClient = new();

    private const string Token = "c097dd31c55d5549260e62b69bad114389cf54459a4a5fd58dedf9165a6d3813";

    private const string Private = "b71d53997fcaaaa2b1c9ce005af9c1c6";

    private const string GateUrl = $"https://kiber-one.fun/gate/v1";
    
    private async Task<int> PostDataAsync<T>(T jData, string postUrl)
    {
        var result = 0;

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{GateUrl}/{Private}/{postUrl}");

            request.Headers.Authorization = new AuthenticationHeaderValue(Token);

            var serializedData = JsonConvert.SerializeObject(jData);

            request.Content = new StringContent(serializedData, Encoding.UTF8, "application/json");

            using var response = await HttpClient.SendAsync(request);

            var httpStatus = response.StatusCode;

            result = (int)httpStatus;
        }
        catch
        {
            result = -1;
        }

        return result;
    }

    //Проверка состояния сервера
    //ver 1.0.0 (gate-api 1.0.0)
    public async Task<string> CheckServerStatusAsync()
    {
        string status;

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{GateUrl}/{Private}/status");

            request.Headers.Authorization = new AuthenticationHeaderValue(Token);

            using var response = await HttpClient.SendAsync(request);

            var httpStatus = response.StatusCode;

            if (httpStatus == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jContent = JObject.Parse(content);

                var dataStatus = jContent["status"]?.ToString();

                status = dataStatus == "ok" ? "Online" : "MainError";
            }
            else
            {
                status = "GateError";
            }
        }
        catch
        {
            status = "GateError";
        }

        return status;
    }


    //Работа с данными пользователя//
    //ver 1.0.0 (api 2.0.0)
    public async Task<int> ChangeUserPasswordAsync(Pass pass, string newPassword)
    {
        int resultStatus;

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{GateUrl}/{Private}/user/change-password");

            request.Headers.Authorization = new AuthenticationHeaderValue(Token);

            var jNewPass = Mapper.PassToJPass(pass);
            jNewPass.password = newPassword;

            var stringjNewPass = JsonConvert.SerializeObject(jNewPass);

            request.Content = new StringContent(stringjNewPass, Encoding.UTF8, "application/json");

            using var response = await HttpClient.SendAsync(request);

            var httpStatus = response.StatusCode;

            if (httpStatus == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jContent = JObject.Parse(content);

                var dataStatus = jContent["status"].ToString();

                if (dataStatus == "ok") pass.Password = newPassword;
            }

            resultStatus = (int)httpStatus;
        }
        catch
        {
            resultStatus = -1;
        }

        return resultStatus;
    }

    //ver 1.0.0 (api 2.0.0)
    public async Task<(Pass, int)> LoginUserAsync(string login, string password)
    {
        var resultPass = new Pass();
        var resultStatus = 0;

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{GateUrl}/{Private}/user/auth");

            request.Headers.Authorization = new AuthenticationHeaderValue(Token);
            request.Headers.Add("login", login);
            request.Headers.Add("password", password);

            using var response = await HttpClient.SendAsync(request);

            var httpStatus = response.StatusCode;

            if (httpStatus == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jContent = JObject.Parse(content);

                var dataStatus = jContent["status"].ToString();

                if (dataStatus == "ok")
                {
                    var jPass = JsonConvert.DeserializeObject<JPass>(jContent["data"].ToString());

                    var pass = Mapper.JPassToPass(jPass);

                    resultPass = pass;
                    resultStatus = 200;
                }
                else if (dataStatus == "access_denied")
                {
                    resultStatus = 200403;
                }
                else
                {
                    resultStatus = 200404;
                }
            }
            else
            {
                resultStatus = (int)httpStatus;
            }
        }
        catch
        {
            resultStatus = -1;
        }

        return (resultPass, resultStatus);
    }

    //ver 1.0.0 - Загрузка профиля
    public async Task<(Profile, int)> LoadProfileDataAsync(int userId)
    {
        var resultProfile = new Profile();
        var resultStatus = 0;

        try
        {
            using var request =
                new HttpRequestMessage(HttpMethod.Get, $"{GateUrl}/{Private}/user/resident/{userId}/profile");

            request.Headers.Authorization = new AuthenticationHeaderValue(Token);

            using var response = await HttpClient.SendAsync(request);

            var httpStatus = response.StatusCode;

            if (httpStatus == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jContent = JObject.Parse(content);

                var dataStatus = jContent["status"].ToString();

                if (dataStatus == "ok")
                {
                    var jProfile = JsonConvert.DeserializeObject<JProfile>(jContent["data"].ToString());

                    var profile = Mapper.JProfileToProfile(jProfile, GateUrl);

                    resultProfile = profile;
                    resultStatus = 200;
                }
                else
                {
                    resultStatus = (int)httpStatus;
                }
            }
        }
        catch
        {
            resultStatus = -1;
        }

        return (resultProfile, resultStatus);
    }

    //---------------------------//

    //Загрузка данных магазина//
    public async Task<(ObservableCollection<Item>, int)> LoadCityShopDataAsync(int cityId)
    {
        ObservableCollection<Item> resultItemsList = new();
        var resultStatus = 0;

        try
        {
            using var request =
                new HttpRequestMessage(HttpMethod.Get, $"{GateUrl}/{Private}/city/{cityId}/kibershop");

            request.Headers.Authorization = new AuthenticationHeaderValue(Token);

            using var response = await HttpClient.SendAsync(request);

            var httpStatus = response.StatusCode;

            if (httpStatus == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jContent = JObject.Parse(content);

                var dataStatus = jContent["status"].ToString();

                if (dataStatus == "ok")
                {
                    var jItemsList = JArray.Parse(jContent["data"].ToString());

                    for (var i = 0; i < jItemsList.Count; i++)
                    {
                        var jItem = JsonConvert.DeserializeObject<JItem>(jContent["data"][i].ToString());
                        if (jItem.active != 1) continue;
                        var item = Mapper.JItemToItem(jItem, GateUrl);
                        resultItemsList.Add(item);
                    }
                }
            }
        }
        catch
        {
            resultStatus = -1;
        }

        return (resultItemsList, resultStatus);
    }
    //---------------------------//


    //Загрузка данных новостной ленты//
    public async Task<(ObservableCollection<News>, int)> LoadCityFeedDataAsync(int cityId)
    {
        ObservableCollection<News> resultNewsList = new();
        var resultStatus = 0;

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{GateUrl}/{Private}/city/{cityId}/feed");

            request.Headers.Authorization = new AuthenticationHeaderValue(Token);

            using var response = await HttpClient.SendAsync(request);

            var httpStatus = response.StatusCode;

            if (httpStatus == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jContent = JObject.Parse(content);

                var dataStatus = jContent["status"].ToString();

                if (dataStatus == "ok")
                {
                    var jNewsList = JArray.Parse(jContent["data"].ToString());

                    for (var i = 0; i < jNewsList.Count; i++)
                    {
                        var jNews = JsonConvert.DeserializeObject<JNews>(jNewsList[i].ToString());
                        if (jNews.active != 1) continue;
                        var news = Mapper.JNewsToNews(jNews, GateUrl);
                        resultNewsList.Add(news);
                    }
                }
                else
                {
                    resultStatus = 404;
                }
            }
        }
        catch
        {
            resultStatus = -1;
        }

        return (resultNewsList, resultStatus);
    }
    //---------------------------//


    //Загрузка логов изменения баланса//
    public async Task<(ObservableCollection<KiberonLog>, int)> LoadKiberonLogsDataStoreAsync(int userId)
    {
        var resultLogsList = new ObservableCollection<KiberonLog>();
        var resultStatus = 0;

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get,
                $"{GateUrl}/{Private}/user/resident/{userId}/kiberon-logs");

            request.Headers.Authorization = new AuthenticationHeaderValue(Token);

            using var response = await HttpClient.SendAsync(request);

            var httpStatus = response.StatusCode;

            if (httpStatus == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jContent = JObject.Parse(content);

                var dataStatus = jContent["status"].ToString();

                if (dataStatus == "ok")
                {
                    var jLogsList = JArray.Parse(jContent["data"].ToString());

                    if (jLogsList.Count == 0)
                        resultStatus = 404;
                    else
                        foreach (var t in jLogsList)
                        {
                            var jLog = JsonConvert.DeserializeObject<JKiberonLog>(t.ToString());
                            var log = Mapper.JKiberonLogToKiberonLog(jLog);
                            resultLogsList.Add(log);
                        }
                }
                else
                {
                    resultStatus = 404;
                }
            }
        }
        catch
        {
            resultStatus = -1;
        }

        return (resultLogsList, resultStatus);
    }
    //---------------------------//


    //Формы обратной связи//
    public async Task<int> SendInviteFormAsync(Invite inviteForm)
    {
        int resultStatus;

        try
        {
            const string postUrl = "user/resident/form/invite-friend";

            var jData = Mapper.InviteFormToJInviteForm(inviteForm);

            resultStatus = await PostDataAsync(jData, postUrl);
        }
        catch
        {
            resultStatus = -1;
        }

        return resultStatus;
    }

    public async Task<int> SendFeedbackFormAsync(Request requestForm)
    {
        var resultStatus = 0;

        try
        {
            const string postUrl = "user/resident/form/feedback";

            var jData = Mapper.RequestFormToJRequestForm(requestForm);

            resultStatus = await PostDataAsync(jData, postUrl);
        }
        catch
        {
            resultStatus = -1;
        }

        return resultStatus;
    }

    public async Task<int> SendCallbackFormAsync(Callback callbackForm)
    {
        var resultStatus = 0;

        try
        {
            const string postUrl = "user/resident/form/callback";

            var jData = Mapper.CallbackFormToJCallbackForm(callbackForm);

            resultStatus = await PostDataAsync(jData, postUrl);
        }
        catch
        {
            resultStatus = -1;
        }

        return resultStatus;
    }

    public async Task<int> SendTechSupportFormAsync(TechSupport techSupportForm)
    {
        var resultStatus = 0;

        try
        {
            const string postUrl = "user/resident/form/tech-support-request";

            var jData = Mapper.TechSupportFormToJTechSupportForm(techSupportForm);

            resultStatus = await PostDataAsync(jData, postUrl);
        }
        catch
        {
            resultStatus = -1;
        }

        return resultStatus;
    }

    //---------------------------//
}