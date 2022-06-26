using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Clients
{
    public class AzureApiClient : IAzureApiClient
    {
        private readonly string _baseApiUrl = "https://practicalshooterappfunctionsapi.azurewebsites.net/api/";
        private readonly HttpClient _httpClient;
        
        public AzureApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseApiUrl);
            _httpClient.Timeout = new TimeSpan(0, 0, 10);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<InAppMessageModel>> GetInAppMessages()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("getInAppMessages");
                var deserializedResponse = JsonConvert.DeserializeObject<List<InAppMessageModel>>(response.Content.ReadAsStringAsync().Result);

                return deserializedResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<InAppMessageModel>();
            }
        }

        public async Task<List<HomeTilesModel>> GetHomeTiles()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("getHomeTiles");
                var deserializedResponse = JsonConvert.DeserializeObject<List<HomeTilesModel>>(response.Content.ReadAsStringAsync().Result);

                return deserializedResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<HomeTilesModel>();
            }
        }

        public async Task<List<CalendarEntriesModel>> GetCalendarEntries()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("getCalendarEntries");
                var deserializedResponse = JsonConvert.DeserializeObject<List<CalendarEntriesModel>>(response.Content.ReadAsStringAsync().Result);

                return deserializedResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<CalendarEntriesModel>();
            }
        }
    }
}