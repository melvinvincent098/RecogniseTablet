using Prism;
using Prism.Ioc;
using RecogniseTablet.Helper;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Managers;
using RecogniseTablet.ViewModels;
using RecogniseTablet.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RecogniseTablet
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            APIHelper.InitializeClient();

            await NavigationService.NavigateAsync("RootNavPage/AddFacePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //View to ViewModel Binding
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RootNavPage, RootNavPageViewModel>();
            containerRegistry.RegisterForNavigation<AddFacePage, AddFacePageViewModel>();

            //Manager Interface to Manager Binding
            containerRegistry.RegisterSingleton<IApplicationManager, ApplicationManager>();
            containerRegistry.RegisterSingleton<IUserManager, UserManager>();
        }
    }
}
