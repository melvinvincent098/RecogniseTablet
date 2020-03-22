using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;
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

namespace RecogniseTablet.Managers
{
    public class FaceManager: IFaceManager
    {
        public FaceManager()
        {

        }

        public async Task DetectFace(string filePath)
        {
            byte[] byteData = GetImageAsByteArray(filePath);

            FaceFileModel fileobj = new FaceFileModel()
            {
                byteDataFile = byteData
            };

            var file = JsonConvert.SerializeObject(fileobj);
            var stringContent = new StringContent(file, UnicodeEncoding.UTF8, "application/json");
            string url = $"Face/IdentifyFace/";

             HttpResponseMessage response = await APIHelper.ApiClient.PostAsync(url, stringContent);

            var b = response.Content.ReadAsAsync<IdentifyResult[]>();

            var a = b.Result;

          
            
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
