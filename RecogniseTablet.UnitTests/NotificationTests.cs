using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.DeepObjectCompare;
using NUnit.Framework;
using RecogniseTablet.Helper;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Managers;

namespace RecogniseTablet.UnitTests
{
    public class NotificationTests
    {
        private IApplicationManager _applicationManager;

        [SetUp]
        public void Setup()
        {
            APIHelper.InitializeClient();
            _applicationManager = new ApplicationManager(new UserManager(), new FaceManager(), new CameraManager(), new NotificationManager(), new LocationManager());
        }

        [Test]
        public async Task SendNotification_Valid()
        {
            //Arrange


            //Act
            await this._applicationManager.NotificationManager.SendNotification();

            //Assert
            Assert.Pass();

        }
    }
}
