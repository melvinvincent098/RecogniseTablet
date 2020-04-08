using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.Interfaces
{
    public interface ICameraManager
    {
        event EventHandler<byte[]> CameraFaceDetect;

        void OnCameraFaceDetect(byte[] CameraFaceData);

    }
}
