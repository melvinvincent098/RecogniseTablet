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

namespace RecogniseTablet.ViewModels
{
   public class AddFacePageViewModel: ViewModelBase
    {
        private ImageSource _imageLocation;
        public DelegateCommand<string> TakePhotoCommand { get; set; }
        public AddFacePageViewModel(INavigationService navigationService, IApplicationManager applicationManager) : base(navigationService, applicationManager)
        {
            this.TakePhotoCommand = new DelegateCommand<string>(async login => await this.TakePhotoCommandMethod());
        }

        public async Task TakePhotoCommandMethod()
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

            await this.ApplicationManager.FaceManager.DetectFace(albumpath);

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
    }
}
