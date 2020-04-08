using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.Managers
{
    public class CameraManager:ICameraManager
    {
        public event EventHandler<byte[]> CameraFaceDetect;

        public void OnCameraFaceDetect(byte[] CameraFaceData)
        {
            var e = this.CameraFaceDetect;
            e?.Invoke(this, CameraFaceData);
        }
    }
}
