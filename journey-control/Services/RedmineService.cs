using journey_control.Models;
using Newtonsoft.Json;

namespace journey_control.Services
{
    public class RedmineService
    {
        private readonly string baseUrl = "https://redmine.questor.com.br";

        public async Task<User> GetUserAsync(string apiKey)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", apiKey);

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/users/current.json");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var userResponse = JsonConvert.DeserializeObject<dynamic>(json);
                    return JsonConvert.DeserializeObject<User>(userResponse.user.ToString());
                }
                return null;
            }
        }
    }
}
