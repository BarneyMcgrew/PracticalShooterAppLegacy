using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Akavache;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PracticalShooterApp.Shared.Services
{
    public class WebCacheService
    {
        private DateTimeOffset _cacheExpiry = DateTimeOffset.Now.AddDays(7);
        
        public WebCacheService()
        {
            
        }

        public async Task<Byte[]> GetWebImageCache(string imageUrl)
        {
            Byte[] imageByte = null;
            
            Func<string, Byte[]> getWebImage = GetWebImage; 

            imageByte = await BlobCache.LocalMachine.GetOrCreateObject<Byte[]>(imageUrl, () => getWebImage(imageUrl),
                _cacheExpiry);

            if (imageByte == null)
            {
                imageByte = GetWebImage(imageUrl);
                await BlobCache.LocalMachine.InsertObject(imageUrl, imageByte, _cacheExpiry);
            }

            return imageByte;
        }

        private byte[] GetWebImage(string imageUrl)
        {
            byte[] imageByte = null;
            
            using (var webClient = new WebClient())
            {
                try
                {
                    imageByte = webClient.DownloadData(new Uri(imageUrl));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
            return imageByte;
        }
    }
}