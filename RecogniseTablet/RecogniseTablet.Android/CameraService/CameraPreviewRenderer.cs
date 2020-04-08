using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Hardware;
using RecogniseTablet.Droid.CameraService;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Linq;
using Java.IO;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Events;

[assembly: ExportRenderer(typeof(RecogniseTablet.CameraOption.CameraPreview), typeof(CameraPreviewRenderer))]
namespace RecogniseTablet.Droid.CameraService
{
    public class CameraPreviewRenderer : ViewRenderer<RecogniseTablet.CameraOption.CameraPreview, RecogniseTablet.Droid.CameraService.CameraPreview>
    {
        CameraPreview cameraPreview;
        private readonly ICameraService cameraService;


        public CameraPreviewRenderer(Context context) : base(context)
        {
            this.cameraService = new CameraDetectService(); 
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RecogniseTablet.CameraOption.CameraPreview> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

                // Unsubscribe
                cameraPreview.Click -= OnCameraPreviewClicked;
            }
            if (e.NewElement != null)
            {

                if (Control == null)
                {
                    cameraPreview = new CameraPreview(Context);
                    SetNativeControl(cameraPreview);
                }
                Control.Preview = Camera.Open((int)e.NewElement.Camera);




                // Subscribe
                cameraPreview.Click += OnCameraPreviewClicked;
                cameraPreview.Preview.FaceDetection += Detect;

            }
        }


        private async void Detect(object sender, Camera.FaceDetectionEventArgs e)
        {

            
            var w = e.Faces.Count();
            if (w > 0)
            {

                //Console.WriteLine("Score: " + e.Faces.First().Score);
                if (e.Faces.First().Score > 90)
                {
                    cameraPreview.Preview.StopPreview();
                    cameraPreview.Preview.FaceDetection -= Detect;
                    cameraPreview.Preview.StopFaceDetection();
                    cameraPreview.Preview.StartPreview();

                    await Task.Delay(3000);
                    cameraPreview.Preview.TakePicture(null, null, (Camera.IPictureCallback)this.cameraService);

                    //  cameraPreview.Preview.StopPreview();
                }
            }
            //cameraPreview.IsPreviewing = false;
        }


        void OnCameraPreviewClicked(object sender, EventArgs e)
        {
            if (cameraPreview.IsPreviewing)
            {
                cameraPreview.Preview.StopPreview();

                cameraPreview.IsPreviewing = false;
            }
            else
            {
                // Console.WriteLine("Start Camera");
                cameraPreview.IsPreviewing = true;
                cameraPreview.Preview.StartPreview();
                cameraPreview.Preview.StartFaceDetection();
                cameraPreview.Preview.FaceDetection += Detect;

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.Preview.Release();
            }
            base.Dispose(disposing);
        }




    }
}