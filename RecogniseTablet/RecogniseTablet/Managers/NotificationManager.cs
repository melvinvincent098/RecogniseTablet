using RecogniseTablet.Helper;
using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.Managers
{
    public class NotificationManager:INotificationManager
    {
        /// <summary>
        /// Sends notification
        /// </summary>
        /// <returns></returns>
        public async Task SendNotification()
        {
            string url = $"Notification/SendMessage";                                       //URI to backend API

            try
            {
                HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url);     //Calls the backend API to send notification

            }
            catch (Exception err)
            {
                                                                                            //Notification didnt send
            }

        }
    }
}
