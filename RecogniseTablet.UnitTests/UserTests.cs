using NUnit.Framework;
using RecogniseTablet.Helper;
using RecogniseTablet.Interfaces;
using RecogniseTablet.Managers;
using RecogniseTablet.Models;
using System.Threading.Tasks;

namespace RecogniseTablet.UnitTests
{
    public class UserTests
    {
        private IApplicationManager _applicationManager;

        [SetUp]
        public void Setup()
        {
            APIHelper.InitializeClient();
            _applicationManager = new ApplicationManager(new UserManager(), new FaceManager(), new CameraManager(), new NotificationManager(),new LocationManager());
        }

        [Test]
        public async Task CheckUser_ValidUserValidPassword_ReturnsUser()
        {
            //Arrange

            var username = "test";
            var password = "test";

            var expectedUser = new UserModel
            {
                ID = 35,
                FirstName = "test",
                Surname = "test",
                Email = "b6006976@my.shu.ac.uk",
                Password = "test",
                Username = "test"

            };

            //Act
            var result = await this._applicationManager.UserManager.CheckUser(username, password);

            //Assert
            Assert.That(result, NUnit.DeepObjectCompare.Is.DeepEqualTo(expectedUser));
        }

        [Test]
        public async Task CheckUser_ValidUserInValidPassword_Null()
        {
            //Arrange

            var username = "test";
            var password = "123";


            //Act
            var result = await this._applicationManager.UserManager.CheckUser(username, password);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task CheckUser_InValidUser_Null()
        {
            //Arrange

            var username = "invalid";
            var password = "123";

            //Act
            var result = await this._applicationManager.UserManager.CheckUser(username, password);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task CheckUserIDPersonGroupID_Valid_PersonGroupID()
        {
            //Arrange

            var userId = 35;

            //Act
            var result = await this._applicationManager.UserManager.CheckUserIDPersonGroupID(userId);

            //Assert
            Assert.AreEqual(35, result);
        }

        [Test]
        public async Task CheckUserIDPersonGroupID_Invalid_Returns0()
        {
            //Arrange
            var userId = 0;

            //Act
            var result = await this._applicationManager.UserManager.CheckUserIDPersonGroupID(userId);

            //Assert
            Assert.AreEqual(0, result);
        }
    }
}