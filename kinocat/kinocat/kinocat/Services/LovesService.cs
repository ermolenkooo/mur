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
    public class LovesService
    {
        const string Url = "http://192.168.0.59:5000/api/Loves/";
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<Love>> Get(int id)
        {
            string result = await App.client.GetStringAsync(Url + id);
            return JsonSerializer.Deserialize<IEnumerable<Love>>(result, options);
        }

        public async Task<Love> Add(Love l)
        {
            var response = await App.client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(l),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Love>(
                await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<Love> Delete(int userid, int filmid)
        {
            var response = await App.client.DeleteAsync(Url + userid + "/" + filmid);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Love>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
