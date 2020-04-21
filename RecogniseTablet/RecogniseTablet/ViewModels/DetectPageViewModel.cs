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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            PersonGroupID = parameters.GetValue<int>("personGroupID");
            UserId = parameters.GetValue<int>("userId");
            IsProcessing = false;
        }


        public async void CameraManager_CameraScan(object sender, byte[] data)
        {
            IsProcessing = true;
            var result = await this.ApplicationManager.FaceManager.IdentifyFace(data, PersonGroupID.ToString());

            if (!result)
            {
                await this.ApplicationManager.NotificationManager.SendNotification();               //Sends alert to other device
                await this.ApplicationManager.LocationManager.GetLocation(UserId);
                SetupLocationTimer();
                aTimer.Elapsed += GetLocationRepeat;
                IsProcessing = false;
            }
            else
            {
                IsProcessing = false;
                await this._dialogService.DisplayAlertAsync("Hello", "Everything is ok, your are the registered driver!", "Ok");
            }

            
        }

        public void SetupLocationTimer()
        {
            aTimer = new Timer();
            aTimer.Interval = 60000;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void GetLocationRepeat(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => await this.ApplicationManager.LocationManager.GetLocation(UserId));
        }

        public void OnAppearing()
        {
            MessagingCenter.Subscribe<ICameraService, byte[]>(this, "FaceData", async (sender, arg) =>
            {
                this.ApplicationManager.CameraManager.OnCameraFaceDetect(arg);
            });

                       
            this.ApplicationManager.CameraManager.CameraFaceDetect += CameraManager_CameraScan;
        }



        public void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ICameraService, byte[]>(this, "FaceData");

            this.ApplicationManager.CameraManager.CameraFaceDetect -= CameraManager_CameraScan;
            aTimer.Elapsed -= GetLocationRepeat;
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
