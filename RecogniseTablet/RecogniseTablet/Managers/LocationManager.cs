using Newtonsoft.Json;
using RecogniseTablet.Helper;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RecogniseTablet.Managers
{
    public class LocationManager:ILocationManager
    {
        public async Task<LocationModel> SaveLocation(LocationModel location)
        {

            string url = $"Location/SaveLocation/";

            try
            {
                //convert user object into json
                var json = JsonConvert.SerializeObject(location);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                //Post the user
                var response = await APIHelper.ApiClient.PostAsync(url, stringContent);
                return await response.Content.ReadAsAsync<LocationModel>();
            }
            catch       //if error then return 0 as ID
            {
                return null;
            }
        }

        public async Task GetLocation(int UserId)
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            var foundLocation = new LocationModel()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                UserId = UserId
            };

            await SaveLocation(foundLocation);
        }
    }

}
