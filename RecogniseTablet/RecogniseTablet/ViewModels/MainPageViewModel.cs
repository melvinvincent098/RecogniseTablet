using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Models;
using RecogniseTablet.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand<string> DoAddFaceCommand { get; set; }
        IEnumerable<UserModel> LoginUser;
        private string _userId, _username, _name;


        public MainPageViewModel(INavigationService navigationService, IApplicationManager applicationManager) : base(navigationService, applicationManager)
        {
            Title = "Main Page";
            this.DoAddFaceCommand = new DelegateCommand<string>(async login => await this.DoAddFaceCommandMethod());

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoginUser = parameters.GetValues<UserModel>("user");
            UserId = LoginUser.First().ID.ToString();
            Username = LoginUser.First().Username;
            Name = LoginUser.First().FirstName;


        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }


        public async Task DoAddFaceCommandMethod()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("userid", UserId);
            navigationParams.Add("name", Name);
            navigationParams.Add("username", Username);
            await this.NavigationService.NavigateAsync(nameof(AddFacePage),navigationParams);

        }

        public string UserId
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

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                this.SetProperty(ref this._username, value);
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                this.SetProperty(ref this._name, value);
            }
        }
    }
}
