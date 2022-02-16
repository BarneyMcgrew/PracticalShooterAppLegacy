using Newtonsoft.Json;
using PracticalShooterApp.Clients.Interfaces;
using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PracticalShooterApp.Clients
{
    public class RSSApiClient : IRSSApiClient
    {
        private readonly HttpClient _httpClient;

        public RSSApiClient()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            _httpClient = new HttpClient(handler);            
            _httpClient.Timeout = new TimeSpan(0, 0, 10);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<RSSRawFeed> GetRSSFeed(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                XmlSerializer serializer = new XmlSerializer(typeof(RSSRawFeed));
                using (StringReader reader = new StringReader(response.Content.ReadAsStringAsync().Result))
                {
                    var deserializedResponse = (RSSRawFeed)serializer.Deserialize(reader);

                    return deserializedResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new RSSRawFeed();
            }
        }
    }
}
