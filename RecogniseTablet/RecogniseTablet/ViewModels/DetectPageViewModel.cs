using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RecogniseTablet.Events;
using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RecogniseTablet.ViewModels
{
    public class DetectPageViewModel: ViewModelBase, IPageLifecycleAware
    {
        private readonly ICameraService _cameraService;
        private int _personGroupID, _userId;
        private readonly IPageDialogService _dialogService;
        public static Timer aTimer;
        public DetectPageViewModel(INavigationService navigationService, IApplicationManager applicationManager, ICameraService cameraService, IPageDialogService dialogService) : base(navigationService, applicationManager, dialogService)
        {
            Title = "Detect Face";
            _cameraService = cameraService;
            _dialogService = dialogService;
            ActivityIndicator activityIndicator = new ActivityIndicator();
        }

        /// <summary>
        /// Hits the OnNavigateTo function when page loads
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            PersonGroupID = parameters.GetValue<int>("personGroupID");
            UserId = parameters.GetValue<int>("userId");
            IsProcessing = false;
        }

        /// <summary>
        /// Subscribed event when face is detected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public async void CameraManager_CameraScan(object sender, byte[] data)
        {
            IsProcessing = true;                                                                                                    //Loading spinner shows
            var result = await this.ApplicationManager.FaceManager.IdentifyFace(data, PersonGroupID.ToString());                    //calls the face manager to identify face

            if (!result)                                                                                                            //unknown user found
            {
                await this.ApplicationManager.NotificationManager.SendNotification();                                               //Sends alert to other device
                await this.ApplicationManager.LocationManager.GetLocation(UserId);                                                  //Gets location of tablet as it is unknown user
                SetupLocationTimer();                                                                                               //sets a timer up so it updates every min
                aTimer.Elapsed += GetLocationRepeat;                                                                                //subscribe to timer event so when time runs out, it calls GetLocationRequest
                IsProcessing = false;                                                                                               //Hides loading spinner
            }
            else                                                                                                                    //User is the found and matches registered face
            {
                IsProcessing = false;                                                                                               //hides loading spinner
                await this._dialogService.DisplayAlertAsync("Hello", "Everything is ok, your are the registered driver!", "Ok");
            }

            
        }

        /// <summary>
        /// Timer setup
        /// </summary>
        public void SetupLocationTimer()
        {
            aTimer = new Timer();
            aTimer.Interval = 60000;                                                                                                //Sets timer to 1 min (60000 millisecond)
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void GetLocationRepeat(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => await this.ApplicationManager.LocationManager.GetLocation(UserId));
        }

        public void OnAppearing()
        {
            MessagingCenter.Subscribe<ICameraService, byte[]>(this, "FaceData", async (sender, arg) =>                          //Subscribe to camera face detect event
            {
                this.ApplicationManager.CameraManager.OnCameraFaceDetect(arg);                                                  //when the event is trigger, OnCameraDetect event is triggered
            });

                       
            this.ApplicationManager.CameraManager.CameraFaceDetect += CameraManager_CameraScan;                                 //Subscribing to CameraFaceDetect event
        }



        public void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ICameraService, byte[]>(this, "FaceData");                                          //Unsubscribe from evenrt

            this.ApplicationManager.CameraManager.CameraFaceDetect -= CameraManager_CameraScan;
            aTimer.Elapsed -= GetLocationRepeat;                                                                            //unsubscribe from timer
        }



        public int PersonGroupID
        {
            get
            {
                return _personGroupID;
            }

            set
            {
                this.SetProperty(ref this._personGroupID, value);
            }
        }

        public int UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                this.SetProperty(ref this._userId, value);
            }
        }
    }
}
