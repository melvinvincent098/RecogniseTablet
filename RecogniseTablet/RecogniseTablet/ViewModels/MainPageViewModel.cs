using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecogniseTablet.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IApplicationManager applicationManager) : base(navigationService, applicationManager)
        {
            Title = "Main Page";
        }
    }
}
