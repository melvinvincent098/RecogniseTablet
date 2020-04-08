using RecogniseTablet.Helper;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.Managers
{
    public class UserManager:IUserManager
    {
        public async Task<UserModel> CheckUser(string username, string password)
        {
            string url = $"User/GetUserByUsername/{username}";
            try
            {
                using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
                {
                    UserModel user = new UserModel();

                    if (response.IsSuccessStatusCode)
                    {
                        user = await response.Content.ReadAsAsync<UserModel>();
                        if (user.Password == password)                                                                           //Check if the password matches
                        {
                            return user;
                        }
                        else
                        {
                            return null;                                                                                      //password does not match so returns a false
                        }
                    }
                    else
                    {
                        return null;                                                                                           //also returns a false if response from server is not successful

                    }
                }
            }
            catch(Exception err)
            {
                return null;                                                                                                   //username cannot be found or server error
            }

        }

        public async Task<int> CheckUserIDPersonGroupID(int UserID)
        {
            string url = $"User/UserIDPersonGroupID/{UserID}";
            int personGroupID = 0;
            try
            {
                HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url);
                
                    

                    if (response.IsSuccessStatusCode)
                    {
                       personGroupID = await response.Content.ReadAsAsync<int>();
                        return personGroupID;
                        
                    }
                    else
                    {
                        return personGroupID;                                                                                           //also returns a 0 if response from server is not successful

                    }
                
            }
            catch (Exception err)
            {
                return personGroupID;                                                                                                   //returns 0 as a personGroupID
            }

        }

        public async Task InsertUserIDPersonGroupID(int UserID, int PersonGroupID)
        {
            

            try
            {
                string url = $"User/UserIDPersonGroupID/{UserID}/{PersonGroupID}";

                HttpResponseMessage response = await APIHelper.ApiClient.PostAsync(url, null);

            }
            catch (Exception err)
            {
                                                                                                      
            }

        }
    }
}
