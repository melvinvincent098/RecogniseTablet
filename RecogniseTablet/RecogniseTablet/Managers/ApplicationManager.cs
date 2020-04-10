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

        public ILocationManager LocationManager => this.locationManager;

        private readonly IUserManager userManager;
        private readonly IFaceManager faceManager;
        private readonly ICameraManager cameraManager;
        private readonly INotificationManager notificationManager;
        private readonly ILocationManager locationManager;

        public ApplicationManager(IUserManager userManager, IFaceManager faceManager, ICameraManager cameraManager, INotificationManager notificationManager, ILocationManager locationManager)
        {
            this.userManager = userManager;
            this.faceManager = faceManager;
            this.cameraManager = cameraManager;
            this.notificationManager = notificationManager;
            this.locationManager = locationManager;
        }

    }
}
