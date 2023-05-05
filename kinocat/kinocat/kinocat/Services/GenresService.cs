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
    public class GenresService
    {
        string Url = "http://" + App.ip + ":5000/api/Genres/"; // обращайте внимание на конечный слеш
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<Genre>> Get()
        {
            //HttpClient client = GetClient();
            string result = await App.client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Genre>>(result, options);
        }

        public async Task<Genre> GetID(int id)
        {
            var response = await App.client.GetAsync(Url + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Genre>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
