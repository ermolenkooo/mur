using kinocat.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace kinocat.Services
{
    public class WatchlistsService
    {
        const string Url = "http://192.168.0.59:5000/api/Watchlists/";
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<Watchlist>> Get(int id)
        {
            string result = await App.client.GetStringAsync(Url + id);
            return JsonSerializer.Deserialize<IEnumerable<Watchlist>>(result, options);
        }

        public async Task<Watchlist> Add(Watchlist w)
        {
            var response = await App.client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(w),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Watchlist>(
                await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<Watchlist> Delete(int userid, int filmid)
        {
            var response = await App.client.DeleteAsync(Url + userid + "/" + filmid);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Watchlist>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
