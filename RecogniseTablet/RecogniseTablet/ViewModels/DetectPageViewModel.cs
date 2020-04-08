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
using Xamarin.Forms;

namespace RecogniseTablet.ViewModels
{
    public class DetectPageViewModel: ViewModelBase, IPageLifecycleAware
    {
        private readonly ICameraService _cameraService;
        private int _personGroupID;
        private readonly IPageDialogService _dialogService;
        public DetectPageViewModel(INavigationService navigationService, IApplicationManager applicationManager, ICameraService cameraService, IPageDialogService dialogService) : base(navigationService, applicationManager, dialogService)
        {
            _cameraService = cameraService;
            _dialogService = dialogService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            PersonGroupID = parameters.GetValue<int>("personGroupID");
        }


        public async void CameraManager_CameraScan(object sender, byte[] data)
        {

            var result = await this.ApplicationManager.FaceManager.IdentifyFace(data, PersonGroupID.ToString());

            if (!result)
            {
                await this.ApplicationManager.NotificationManager.SendNotification();               //Sends alert to other device
            }
            else
            {
                await this._dialogService.DisplayAlertAsync("Hello", "Everything is ok, your are the regitsered driver!", "Ok");
            }

            
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
    }
}
