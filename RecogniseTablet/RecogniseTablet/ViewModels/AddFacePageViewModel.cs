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

namespace RecogniseTablet.ViewModels
{
   public class AddFacePageViewModel: ViewModelBase
    {
        private ImageSource _imageLocation;
        private string _foundUser = string.Empty;
        public DelegateCommand<string> TakePhotoRegisterCommand { get; set; }
        public DelegateCommand<string> TakePhotoIdentifyCommand { get; set; }

        private string _personGroupID, _username, _name;
        public AddFacePageViewModel(INavigationService navigationService, IApplicationManager applicationManager) : base(navigationService, applicationManager)
        {
            this.TakePhotoRegisterCommand = new DelegateCommand<string>(async login => await this.TakePhotoRegisterCommandMethod());
            this.TakePhotoIdentifyCommand = new DelegateCommand<string>(async login => await this.TakePhotoIdentifyCommandMethod());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _personGroupID = parameters.GetValues<string>("userid").First();
            _username = parameters.GetValues<string>("username").First();
            _name = parameters.GetValues<string>("name").First();
        }

        public async Task TakePhotoRegisterCommandMethod()
        {
            //await CrossMedia.Current.Initialize();

            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
            var permissionsStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //*****NEED TO ALERT THE USER THAT CAMERA IS NOT AVAILABLE*****//
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    //Directory = "Sample",
                    //Name = "test.jpg"
                });

            if(file == null)
            {
                return;
            }

            var albumpath = file.AlbumPath;

            ImageLocation = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

            await this.ApplicationManager.FaceManager.RegisterFace(albumpath,_personGroupID,_username,_name);

        }


        public async Task TakePhotoIdentifyCommandMethod()
        {
            //await CrossMedia.Current.Initialize();

            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
            var permissionsStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //*****NEED TO ALERT THE USER THAT CAMERA IS NOT AVAILABLE*****//
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    //Directory = "Sample",
                    //Name = "test.jpg"
                });

            if (file == null)
            {
                return;
            }

            var albumpath = file.AlbumPath;

            ImageLocation = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

            var result = await this.ApplicationManager.FaceManager.IdentifyFace(albumpath,_personGroupID);

            if(result)
            {
                FoundUser = "Welcome " + _name + " To Your Car";
            }
            else
            {
                FoundUser = "GO AWAY YOU THIEF!!!";
            }

        }

        public ImageSource ImageLocation
        {
            get
            {
                return _imageLocation;
            }

            set
            {
                this.SetProperty(ref this._imageLocation, value);
            }
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
