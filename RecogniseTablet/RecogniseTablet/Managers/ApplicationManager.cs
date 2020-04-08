using RecogniseTablet.Events;
using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.Managers
{
    public class ApplicationManager : IApplicationManager
    {

        public IUserManager UserManager => this.userManager;

        public IFaceManager FaceManager => this.faceManager;

        public INotificationManager NotificationManager => this.notificationManager;

        public ICameraManager CameraManager { get { return this.cameraManager; } }

        private readonly IUserManager userManager;
        private readonly IFaceManager faceManager;
        private readonly ICameraManager cameraManager;
        private readonly INotificationManager notificationManager;

        public ApplicationManager(IUserManager userManager, IFaceManager faceManager, ICameraManager cameraManager, INotificationManager notificationManager)
        {
            this.userManager = userManager;
            this.faceManager = faceManager;
            this.cameraManager = cameraManager;
            this.notificationManager = notificationManager;
        }

    }
}
