using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Plugin.Media;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;
using RecogniseTablet.Droid.CameraService;
using RecogniseTablet.Interfaces;
using System;

namespace RecogniseTablet.Droid
{
    [Activity(Label = "RecogniseTablet", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public MainActivity()
        {
     

        }

        protected async override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            await CrossMedia.Current.Initialize();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {

            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }

    public class AndroidInitializer : IPlatformInitializer
    {

        public AndroidInitializer()
        {


        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterInstance<ICameraService>(new CameraDetectService());

        }
    }


}

