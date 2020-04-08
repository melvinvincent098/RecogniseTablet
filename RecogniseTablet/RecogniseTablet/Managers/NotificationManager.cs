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

        public async Task SendNotification()
        {
            string url = $"Notification/SendMessage";

            try
            {
                HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url);

            }
            catch (Exception err)
            {

            }

        }
    }
}
