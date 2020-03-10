using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.Managers
{
    public class ApplicationManager : IApplicationManager
    {
        public IUserManager UserManager => this.userManager;





        private readonly IUserManager userManager;



        public ApplicationManager(IUserManager userManager)
        {
            this.userManager = userManager;
        }
    }
}
