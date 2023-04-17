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
    public class LettersService
    {
        const string Url = "http://192.168.0.59:5000/api/Letters/";
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<Letter>> GetLettersOfUser(int id)
        {
            string result = await App.client.GetStringAsync(Url + "lettersofuser/" + id);
            return JsonSerializer.Deserialize<IEnumerable<Letter>>(result, options);
        }

        public async Task<IEnumerable<Letter>> GetLettersOfFilm(int id)
        {
            string result = await App.client.GetStringAsync(Url + "lettersoffilm/" + id);
            return JsonSerializer.Deserialize<IEnumerable<Letter>>(result, options);
        }

        public async Task<Letter> Add(Letter l)
        {
            var response = await App.client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(l),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Letter>(
                await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<Letter> Delete(int userid, int filmid)
        {
            var response = await App.client.DeleteAsync(Url + userid + "/" + filmid);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Letter>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
