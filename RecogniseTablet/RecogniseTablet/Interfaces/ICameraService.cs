using RecogniseTablet.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.Interfaces
{
    public delegate void CameraFaceDetectedEventHandler(object sender, CameraFaceReceivedEventArgs e);
   
    public interface ICameraService:IDisposable
    {
        //event CameraFaceDetectedEventHandler CameraFaceReceived;

        event EventHandler<CameraFaceReceivedEventArgs> OnFaceDetect;


        void ReceiveCameraFace(byte[] data);


    }
}
