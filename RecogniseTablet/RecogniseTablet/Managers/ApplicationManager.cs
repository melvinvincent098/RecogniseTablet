using RecogniseTablet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecogniseTablet.Managers
{
    public class ApplicationManager : IApplicationManager
    {
        public IUserManager UserManager => this.userManager;

        public IFaceManager FaceManager => this.faceManager;



        private readonly IUserManager userManager;
        private readonly IFaceManager faceManager;


        public ApplicationManager(IUserManager userManager, IFaceManager faceManager)
        {
            this.userManager = userManager;
            this.faceManager = faceManager;
        }
    }
}
