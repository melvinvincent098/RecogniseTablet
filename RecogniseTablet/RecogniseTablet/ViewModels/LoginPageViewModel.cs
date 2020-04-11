using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecogniseTablet.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public DelegateCommand<string> DoLoginCommand { get; set; }
        string _userName, _password;
        private readonly IPageDialogService _dialogService;
        public LoginPageViewModel(INavigationService navigationService, IApplicationManager applicationManager, ICameraService cameraService, IPageDialogService dialogService) : base(navigationService, applicationManager, dialogService)
        {
            _dialogService = dialogService;
            ActivityIndicator activityIndicator = new ActivityIndicator();
            // For Login
            this.DoLoginCommand = new DelegateCommand<string>(async login => await this.LoginCommandMethod());
        }

        public async Task LoginCommandMethod()
        {           
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(Password))
             {
                await this._dialogService.DisplayAlertAsync("Empty Fields", "Please fill in all the boxes", "Ok");
            }
            else
            {
                IsProcessing = true;
                var result = await this.ApplicationManager.UserManager.CheckUser(userName, Password);

                if (result != null)
                {
                    if (result.ID > 0)
                    {
                        var personGroupID = await this.ApplicationManager.UserManager.CheckUserIDPersonGroupID(result.ID);
                        var navigationParams = new NavigationParameters();


                        if (personGroupID > 0)
                        {
                            navigationParams.Add("personGroupID", personGroupID);
                            navigationParams.Add("userId", result.ID);
                            await this.NavigationService.NavigateAsync($"/{nameof(RootNavPage)}/{nameof(DetectPage)}", navigationParams);
                        }
                        else
                        {
                            navigationParams.Add("user", result);
                            await this.NavigationService.NavigateAsync($"/{nameof(RootNavPage)}/{nameof(MainPage)}", navigationParams);
                        }

                    }
                }
                else
                {
                    IsProcessing = false;
                    await this._dialogService.DisplayAlertAsync("Incorrect Details", "Username or Password is Incorrect", "Ok");
                }
            }


        }


        public string userName
        {
            get
            {
                return this._userName;
            }
            set
            {
                this.SetProperty(ref this._userName, value);
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this.SetProperty(ref this._password, value);
            }
        }


    }
}
