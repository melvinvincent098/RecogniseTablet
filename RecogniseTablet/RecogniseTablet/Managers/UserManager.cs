﻿using RecogniseTablet.Helper;
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
        public async Task<bool> CheckUser(string username, string password)
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
                            return true;
                        }
                        else
                        {
                            return false;                                                                                       //password does not match so returns a false
                        }
                    }
                    else
                    {
                        return false;                                                                                           //also returns a false if response from server is not successful

                    }
                }
            }
            catch(Exception err)
            {
                return false;
            }

        }
    }
}