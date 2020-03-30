using Prism.Commands;
using Prism.Navigation;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public DelegateCommand<string> DoLoginCommand { get; set; }
        string _userName, _password;
        public LoginPageViewModel(INavigationService navigationService, IApplicationManager applicationManager) : base(navigationService, applicationManager)
        {
            // For Login
            this.DoLoginCommand = new DelegateCommand<string>(async login => await this.LoginCommandMethod());
        }

        public async Task LoginCommandMethod()
        {
            var result = await this.ApplicationManager.UserManager.CheckUser(userName, Password);

            if(result != null)
            {
                if (result.ID > 0)
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("user",result);
                    await this.NavigationService.NavigateAsync($"/{nameof(RootNavPage)}/{nameof(MainPage)}",navigationParams);
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
