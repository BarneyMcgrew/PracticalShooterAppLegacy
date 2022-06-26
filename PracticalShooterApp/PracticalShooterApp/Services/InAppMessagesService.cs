using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class InAppMessagesService : IInAppMessagesService
    {
        private IAzureApiClient _apiClient => DependencyService.Get<IAzureApiClient>();

        public InAppMessagesService()
        {
            
        }

        public async Task<List<InAppMessageModel>> GetInAppMessages()
        {
            var inAppMessages = new List<InAppMessageModel>();

            try
            {
                inAppMessages = await BlobCache.LocalMachine.GetObject<List<InAppMessageModel>>("inAppMessages");
            }
            catch (Exception)
            {
                // No Cache Found
            }

            return inAppMessages;
        }

        public async Task<List<InAppMessageModel>> UpdateInAppMessages()
        {
            var inAppMessageList = new List<InAppMessageModel>();

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                inAppMessageList = await _apiClient.GetInAppMessages();
            }

            if (inAppMessageList.Count > 0)
                await BlobCache.LocalMachine.InsertObject("inAppMessages", inAppMessageList, TimeSpan.FromDays(14));

            return inAppMessageList;
        }
    }
}