using kinocat.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace kinocat.Services
{
    public class FilmsService
    {
        string Url = "http://" + App.ip + ":5000/api/Films/"; // обращайте внимание на конечный слеш
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<Film>> Get()
        {
            //HttpClient client = GetClient();
            string result = await App.client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Film>>(result, options);
        }

        public async Task<Film> GetID(int id)
        {
            var response = await App.client.GetAsync(Url + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Film>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
