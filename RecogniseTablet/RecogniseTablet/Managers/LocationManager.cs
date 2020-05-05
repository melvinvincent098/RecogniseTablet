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
        /// <summary>
        /// Saves the tablets location by sending in into the backend API
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<LocationModel> SaveLocation(LocationModel location)
        {

            string url = $"Location/SaveLocation/";                                                                 //URI to call backend to save location

            try
            {
                //convert user object into json
                var json = JsonConvert.SerializeObject(location);                                                   //Converts the location object into a Json
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

        /// <summary>
        /// This function gets the exact location of the tablet device
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task GetLocation(int UserId)
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,                                   //3 settings low, medium and high. I didnt put high as it will take a long time so medium will give me a exact lew time consuming location
                    Timeout = TimeSpan.FromSeconds(30)                                              //checks if location can be found do 30 seconds
                });
            }

            var foundLocation = new LocationModel()                                                 //with the found location, it is stored into a location model with a user id
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                UserId = UserId
            };

            await SaveLocation(foundLocation);                                                      //location data is sent to function above to save
        }
    }

}
