using kinocat.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace kinocat.Services
{
    public class CountriesService
    {
        const string Url = "http://192.168.0.59:5000/api/Countries/"; // обращайте внимание на конечный слеш
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<Country>> Get()
        {
            string result = await App.client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Country>>(result, options);
        }

        public async Task<Country> GetID(int id)
        {
            var response = await App.client.GetAsync(Url + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Country>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
