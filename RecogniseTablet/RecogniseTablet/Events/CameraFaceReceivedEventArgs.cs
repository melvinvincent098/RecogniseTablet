using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.Events
{
    public class CameraFaceReceivedEventArgs: EventArgs
    {
        public CameraFaceReceivedEventArgs(byte[] data)
        {
            this.Data = data;
        }

        public byte[] Data { get; set; }
    }
}
