using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;
using Prism.Navigation;
using RecogniseTablet.Helper;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RecogniseTablet.Managers
{
    public class FaceManager: IFaceManager
    {

        public FaceManager()
        {

        }


        /// <summary>
        /// Calls the backend API to register face
        /// </summary>
        /// <param name="byteData"></param>
        /// <param name="PersonGroupID"></param>
        /// <param name="username"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> RegisterFace(byte[] byteData, string PersonGroupID, string username, string name)
        {

            FaceFileModel fileobj = new FaceFileModel()                                                                     //Puts the byte array into a model
            {
                byteDataFile = byteData
            };

            var file = JsonConvert.SerializeObject(fileobj);                                                                //that model is converted into a json
            var stringContent = new StringContent(file, UnicodeEncoding.UTF8, "application/json");
            string url = $"Face/AddFace/{PersonGroupID}/{username}/{name}";                                                 //URI needed to call the backend API

             HttpResponseMessage response = await APIHelper.ApiClient.PostAsync(url, stringContent);                        //Posts the the data needed into the backend and await response

            if(response.IsSuccessStatusCode)                                                                                //Status code is 200
            {
                return true;                                                                                                //Face has been successfully registered
            }
            else
            {
                return false;                                                                                               //Returns false if we get anything except for status code 200
            }
            
        }

        /// <summary>
        /// Detecting a face function 
        /// </summary>
        /// <param name="byteData"></param>
        /// <param name="PersonGroupID"></param>
        /// <returns></returns>
        public async Task<bool> IdentifyFace(byte[] byteData, string PersonGroupID)
        {
            try
            {

                FaceFileModel fileobj = new FaceFileModel()                                                     //puts the byte array into a objetc
                {
                    byteDataFile = byteData
                };

                var file = JsonConvert.SerializeObject(fileobj);                                                //Converts object into JSON 
                var stringContent = new StringContent(file, UnicodeEncoding.UTF8, "application/json");          
                string url = $"Face/IdentifyFace/{PersonGroupID}";                                              //URI to call backend API

                HttpResponseMessage response = await APIHelper.ApiClient.PostAsync(url, stringContent);         //Does a Post to the backend

                var result = response.Content.ReadAsAsync<IdentifyResult[]>();                                  //puts the response into a result variable

                var candidate = result.Result;                                                                  //gets the cadidate object from the response

                var confidence = candidate.First().Candidates.First().Confidence;                               //puts the cofidence value into a variable

                if (confidence > 0.6)                                                                           //Checking if confidence level exceeds 0.6 (0-1)
                {
                    return true;                                                                                //Registered User has been found
                }
                else
                {
                    return false;                                                                               //Unauthorised user found
                }
            }
            catch
            {
                return false;                                                                                   //Error so Unauthorised user found
            }


        }

        // Returns the contents of the specified file as a byte array.
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
    }

}
