using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Models;
using Xamarin.Forms;

namespace PracticalShooterApp.Services
{
    public class HomeTilesService : IHomeTilesService
    {
        private IAzureApiClient _apiClient => DependencyService.Get<IAzureApiClient>();
        
        public HomeTilesService()
        {
            
        }

        public async Task<List<HomeTilesModel>> GetHomeTiles()
        {            
            var homeTiles = new List<HomeTilesModel>();

            try
            {
                homeTiles = await BlobCache.LocalMachine.GetObject<List<HomeTilesModel>>("homeTiles");
            }
            catch (System.Exception)
            {
                // No Cache Found
            }            

            return homeTiles;
        }

        public async Task<List<HomeTilesModel>> UpdateHomeTiles()
        {
            var tileList = await _apiClient.GetHomeTiles();

            if (tileList.Count > 0)
                await BlobCache.LocalMachine.InsertObject("homeTiles", tileList, TimeSpan.FromDays(14));

            return tileList; 
        }
    }
}