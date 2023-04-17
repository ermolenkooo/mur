using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using kinocat.Models;

namespace kinocat.Services
{
    public class SerialsService
    {
        const string Url = "http://192.168.0.59:5000/api/Serials/"; // обращайте внимание на конечный слеш
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<Serial>> Get()
        {
            string result = await App.client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Serial>>(result, options);
        }

        public async Task<Serial> GetID(int id)
        {
            var response = await App.client.GetAsync(Url + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Serial>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
