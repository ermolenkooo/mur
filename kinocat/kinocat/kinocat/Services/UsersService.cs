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
    public class UsersService
    {
        const string Url = "http://192.168.0.59:5000/api/Users/";
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<User>> Get()
        {
            string result = await App.client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<User>>(result, options);
        }

        public async Task<User> Add(User user)
        {
            var response = await App.client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<User>(
                await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<User> GetID(int id)
        {
            var response = await App.client.GetAsync(Url + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<User>(
               await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<User> Login(User user)
        {
            var response = await App.client.PostAsync("http://192.168.0.59:5000/login",
                new StringContent(
                    JsonSerializer.Serialize(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<User>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
