using Prism.Navigation;
using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.ViewModels
{
   public class AddFacePageViewModel: ViewModelBase
    {
        public AddFacePageViewModel(INavigationService navigationService, IApplicationManager applicationManager) : base(navigationService, applicationManager)
        {

        }
    }
}
