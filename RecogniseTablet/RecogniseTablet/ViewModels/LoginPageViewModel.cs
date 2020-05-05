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

        /// <summary>
        /// Login button method
        /// </summary>
        /// <returns></returns>
        public async Task LoginCommandMethod()
        {           
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(Password))                                      //checks if any fields were left empty
            {
                await this._dialogService.DisplayAlertAsync("Empty Fields", "Please fill in all the boxes", "Ok");
            }
            else
            {
                IsProcessing = true;                                                                                            //Show loading spinner
                var result = await this.ApplicationManager.UserManager.CheckUser(userName, Password);                           //calls check user in UserManager and gets back a user model

                if (result != null)                                                                                             //No user has not been found                                                              
                {
                    if (result.ID > 0)                                                                                          //if user exists, id should be > 0
                    {
                        var personGroupID = await this.ApplicationManager.UserManager.CheckUserIDPersonGroupID(result.ID);      //checks if the user has a registered face
                        var navigationParams = new NavigationParameters();


                        if (personGroupID > 0)                                                                                  //persongroupID should be greater than 0 if user has a registered face
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
                else                                                                                                            //username or password is incorrect
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
