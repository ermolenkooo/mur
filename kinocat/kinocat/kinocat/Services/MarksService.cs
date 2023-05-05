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
    public class MarksService
    {
        string Url = "http://" + App.ip + ":5000/api/Marks/";
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<MarkOfUser>> GetMarksOfUser(int id)
        {
            string result = await App.client.GetStringAsync(Url + "marksofuser/" + id);
            return JsonSerializer.Deserialize<IEnumerable<MarkOfUser>>(result, options);
        }

        public async Task<IEnumerable<MarkOfUser>> GetMarksOfFilm(int id)
        {
            string result = await App.client.GetStringAsync(Url + "marksoffilm/" + id);
            return JsonSerializer.Deserialize<IEnumerable<MarkOfUser>>(result, options);
        }

        public async Task<MarkOfUser> Add(MarkOfUser m)
        {
            var response = await App.client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(m),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<MarkOfUser>(
                await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<MarkOfUser> Update(MarkOfUser mark)
        {
            var response = await App.client.PutAsync(Url + mark.Id,
                new StringContent(
                    JsonSerializer.Serialize(mark),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<MarkOfUser>(
                await response.Content.ReadAsStringAsync(), options);
        }
    }
}
