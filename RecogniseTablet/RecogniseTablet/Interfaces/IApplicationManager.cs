using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.Interfaces
{
    public interface IApplicationManager
    {
        IUserManager UserManager { get; }
        IFaceManager FaceManager { get; }

        ICameraManager CameraManager { get; }
        //ICameraService CameraService { get; }

        INotificationManager NotificationManager { get; }

    }
}
