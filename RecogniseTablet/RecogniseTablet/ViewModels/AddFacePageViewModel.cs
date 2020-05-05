using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Prism.AppModel;
using Prism.Services;
using RecogniseTablet.Views;

namespace RecogniseTablet.ViewModels
{
   public class AddFacePageViewModel: ViewModelBase, IPageLifecycleAware
    {
        private readonly ICameraService _cameraService;
        private string _foundUser = string.Empty;
        public DelegateCommand<string> TakePhotoRegisterCommand { get; set; }
        public DelegateCommand<string> TakePhotoIdentifyCommand { get; set; }
        private readonly IPageDialogService _dialogService;

        private string _personGroupID, _username, _name;
        public AddFacePageViewModel(INavigationService navigationService, IApplicationManager applicationManager, ICameraService cameraService, IPageDialogService dialogService) : base(navigationService, applicationManager, dialogService)
        {
            Title = "Register Face";
            _cameraService = cameraService;
            _dialogService = dialogService;
            ActivityIndicator activityIndicator = new ActivityIndicator();
        }
        /// <summary>
        /// Hits this function when the page is loaded
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _personGroupID = parameters.GetValues<string>("userid").First();
            _username = parameters.GetValues<string>("username").First();
            _name = parameters.GetValues<string>("name").First();
        }

        /// <summary>
        /// Subscribed event when face is detected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public async void CameraManager_CameraScan(object sender, byte[] data)
        {
            try
            {
                IsProcessing = true;                                                                                                                            //Show loading spinner
                var result = await this.ApplicationManager.FaceManager.RegisterFace(data, _personGroupID, _username, _name);                                    //Sends info to face mager to register face

                if(result)                                                                                                                                      //Face has been succesfully registered
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("personGroupID", _personGroupID);
                    navigationParams.Add("userId", _personGroupID);

                    await this.ApplicationManager.UserManager.InsertUserIDPersonGroupID(Int32.Parse(_personGroupID), Int32.Parse(_personGroupID));              //flag the user as a regitsered user with face
                    await this._dialogService.DisplayAlertAsync("All Done", "Your Face Has Been Successfully Registered", "Ok");            
                    await this.NavigationService.NavigateAsync(nameof(DetectPage), navigationParams);                                                           //go onto detecting a face.

                }
                else                                                                                                                                            //Regitsering face has failed
                {
                    IsProcessing = false;
                    Console.WriteLine("ERROR: User has been not registered!");
                    await this._dialogService.DisplayAlertAsync("Unsuccessful", "Please Try Again", "Ok");
                }
            }
            catch                                                                                                                                               //Regitsering face has failed
            {
                IsProcessing = false;
                Console.WriteLine("ERROR: User has been not registered!");
                await this._dialogService.DisplayAlertAsync("Unsuccessful", "Please Try Again", "Ok");
            }

        }


        public void OnAppearing()
        {
            MessagingCenter.Subscribe<ICameraService, byte[]>(this, "FaceData", async (sender, arg) =>                                                          //When page is loaded, we want to subscribe to the face detection event
            {
                this.ApplicationManager.CameraManager.OnCameraFaceDetect(arg);
            });


            this.ApplicationManager.CameraManager.CameraFaceDetect += CameraManager_CameraScan;                                                                 //if face event trigger then go to cameraManager_CameraScan --subscribe to listen for that event 
        }

        public void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ICameraService, byte[]>(this, "FaceData");                                                                              //Every time we go from this page need to unsubscribe

            this.ApplicationManager.CameraManager.CameraFaceDetect -= CameraManager_CameraScan;                                                                 //unscribe from event
        }

        public string FoundUser
        {
            get
            {
                return _foundUser;
            }

            set
            {
                this.SetProperty(ref this._foundUser, value);
            }
        }
    }
}
