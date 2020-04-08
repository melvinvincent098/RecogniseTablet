using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RecogniseTablet.Droid.CameraService;
using RecogniseTablet.Events;
using RecogniseTablet.Interfaces;
using Xamarin.Forms;

//[assembly: Xamarin.Forms.Dependency(typeof(CameraDetectService))]
namespace RecogniseTablet.Droid.CameraService
{
    public partial class CameraDetectService: Java.Lang.Object, ICameraService, Camera.IPictureCallback
    {
        private bool disposed = false;

        public event EventHandler<CameraFaceReceivedEventArgs> OnFaceDetect;
        public void ReceiveCameraFace(byte[] data)
        {
            MessagingCenter.Send<ICameraService, byte[]>(this, "FaceData", data);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose only
                }

                this.disposed = true;
            }
        }


        /// <summary>
        /// The function thats gets called when a picture is taken
        /// </summary>
        /// <param name="data"></param>
        /// <param name="camera"></param>
        public void OnPictureTaken(byte[] data, Camera camera)
        {
            ReceiveCameraFace(data);
        }
    }
}