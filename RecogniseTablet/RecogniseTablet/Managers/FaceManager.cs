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



        public async Task RegisterFace(string filePath,string PersonGroupID, string username, string name)
        {
            byte[] byteData = GetImageAsByteArray(filePath);

            FaceFileModel fileobj = new FaceFileModel()
            {
                byteDataFile = byteData
            };

            var file = JsonConvert.SerializeObject(fileobj);
            var stringContent = new StringContent(file, UnicodeEncoding.UTF8, "application/json");
            string url = $"Face/AddFace/{PersonGroupID}/{username}/{name}";

             HttpResponseMessage response = await APIHelper.ApiClient.PostAsync(url, stringContent);

    
            
        }

        /// <summary>
        /// Identify Face
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> IdentifyFace(string filePath, string PersonGroupID)
        {
            try
            {
                byte[] byteData = GetImageAsByteArray(filePath);

                FaceFileModel fileobj = new FaceFileModel()
                {
                    byteDataFile = byteData
                };

                var file = JsonConvert.SerializeObject(fileobj);
                var stringContent = new StringContent(file, UnicodeEncoding.UTF8, "application/json");
                string url = $"Face/IdentifyFace/{PersonGroupID}";

                HttpResponseMessage response = await APIHelper.ApiClient.PostAsync(url, stringContent);

                var b = response.Content.ReadAsAsync<IdentifyResult[]>();

                var a = b.Result;

                var c = a.First().Candidates.First().Confidence;

                if (c > 0.6)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
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
