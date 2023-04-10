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
    public class FollowersService
    {
        const string Url = "http://192.168.0.59:5000/api/Followings/";
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<Following>> GetFollowings(int id)
        {
            string result = await App.client.GetStringAsync(Url + "following/" + id);
            return JsonSerializer.Deserialize<IEnumerable<Following>>(result, options);
        }

        public async Task<IEnumerable<Following>> GetFollowers(int id)
        {
            string result = await App.client.GetStringAsync(Url + "followers/" + id);
            return JsonSerializer.Deserialize<IEnumerable<Following>>(result, options);
        }

        public async Task<Following> Add(Following f)
        {
            var response = await App.client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(f),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Following>(
                await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<Following> Delete(int followerid, int followingid)
        {
            var response = await App.client.DeleteAsync(Url + followerid + "/" + followingid);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Following>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
