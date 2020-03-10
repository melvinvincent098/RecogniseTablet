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
        public MainPageViewModel(INavigationService navigationService, IApplicationManager applicationManager) : base(navigationService, applicationManager)
        {
            Title = "Main Page";
            this.DoAddFaceCommand = new DelegateCommand<string>(async login => await this.DoAddFaceCommandMethod());

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoginUser = parameters.GetValues<UserModel>("user");
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }


        public async Task DoAddFaceCommandMethod()
        {

            await this.NavigationService.NavigateAsync($"/{nameof(RootNavPage)}/{nameof(AddFacePage)}");

        }
    }
}
