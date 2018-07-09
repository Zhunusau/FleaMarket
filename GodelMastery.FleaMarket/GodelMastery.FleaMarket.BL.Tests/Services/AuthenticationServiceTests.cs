using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.Helpers;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Services;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;

namespace GodelMastery.FleaMarket.BL.Tests.Services
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IUserStore<ApplicationUser>> userStore;
        private Mock<UserManager<ApplicationUser>> userManager;
        private Mock<IEmailProvider> emailProvider;
        private AuthenticationService underTest;

        [SetUp]
        public void Init()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            userStore = new Mock<IUserStore<ApplicationUser>>();
            userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object);
            emailProvider = new Mock<IEmailProvider>();
            unitOfWork.Setup(x => x.UserManager).Returns(userManager.Object);
            underTest = new AuthenticationService(unitOfWork.Object, emailProvider.Object);
        }

        [Test]
        public void CreateUser_When_UserDto_Is_Null_Then_Should_Throw_Exception()
        {
            //arrange
            UserDto userDto = null;

            //when then
            Assert.ThrowsAsync<ArgumentNullException>(() => underTest.CreateUser(userDto));
        }

        [Test]
        public async Task CreateUser_When_User_With_The_Same_Email_Is_Already_Exist_Then_Should_Return_False()
        {
            //arrange
            var operationDetails = new OperationDetails(false, "User with the same email is already exist", "Email");
            var userDto = new UserDto { Email = "vl.kuzmich.st@gmail.com" };
            var user = new ApplicationUser { Email = "vl.kuzmich.st@gmail.com" };
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            //when
            var actualResult = await underTest.CreateUser(userDto);

            //then
            Assert.AreEqual(actualResult.Succeeded, operationDetails.Succeeded);
            Assert.AreEqual(actualResult.Message, operationDetails.Message);
        }

        [Test]
        public async Task CreateUser_When_User_Is_Not_Exist_Then_Should_Return_True()
        {
            //arrange
            var operationDetails = new OperationDetails(true, "Registration successful", "");
            var userDto = new UserDto { Email = "vl.kuzmich.st@gmail.com", FirstName = "vlad", LastName = "kuzmich", Role = "User", Password = "123321" };
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(null));
            userManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityResult(new List<string>()));

            //when
            var actualResult = await underTest.CreateUser(userDto);

            //then
            Assert.AreEqual(actualResult.Succeeded, operationDetails.Succeeded);
            Assert.AreEqual(actualResult.Message, operationDetails.Message);
        }

        [Test]
        public async Task CreateUser_When_UserManager_Throw_Exception_Then_Should_Return_False()
        {
            //arrange
            var operationDetails = new OperationDetails(false, "Exception message", "");
            var userDto = new UserDto { Email = "vl.kuzmich.st@gmail.com", FirstName = "vlad", LastName = "kuzmich", Role = "User", Password = "123321" };
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(null));
            userManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Exception message"));

            //when
            var actualResult = await underTest.CreateUser(userDto);

            //then
            Assert.AreEqual(actualResult.Succeeded, operationDetails.Succeeded);
            Assert.AreEqual(actualResult.Message, operationDetails.Message);
        }

        [Test]
        public async Task CreateAsync_When_User_Is_Not_Valid_And_Cant_Create_Then_Should_Return_NotEmptyMistakeList()
        {
            //arrange
            var operationDetails = new OperationDetails(false, "", "");
            var userDto = new UserDto { Email = "vl.kuzmich.st@gmail.com" };
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(null));
            userManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityResult(new List<string> { "test1", "test2", "test3" }));

            //when
            var actualResult = await underTest.CreateUser(userDto);

            //then
            Assert.AreEqual(actualResult.Succeeded, operationDetails.Succeeded);
        }

        [Test]
        public void GenerateEmailConfirmationTokenAsync_When_User_Is_Not_Exist_Then_Should_Throw_Exception()
        {
            //arrange
            var email = "vl.kuzmich.st@gmail.com";
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(null));

            //when then
            Assert.ThrowsAsync<ArgumentNullException>(() => underTest.GenerateEmailConfirmationTokenAsync(email));
        }

        [Test]
        public void GenerateEmailConfirmationToken_When_Generated_Code_Is_Null_Then_Should_Throw_Exception()
        {
            //arrange
            var user = new ApplicationUser { Email = "vl.kuzmich.st@gmail.com" };
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            userManager
                .Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<string>(null));

            //when then
            Assert.ThrowsAsync<ArgumentNullException>(() => underTest.GenerateEmailConfirmationTokenAsync(user.Email));
        }

        [Test]
        public async Task GenerateEmailConfirmationToken_When_User_Is_Exist_Then_Should_Return_EmailConfirmationToken()
        {
            //arrange
            var user = new ApplicationUser { Email = "vl.kuzmich.st@gmail.com" };
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            userManager
                .Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<string>()))
                .ReturnsAsync("sometoken");

            var actualResult = await underTest.GenerateEmailConfirmationTokenAsync("vl.kuzmich.st@gmail.com");

            //when then
            Assert.IsTrue(actualResult != null);
        }

        [Test]
        public void ConfirmEmailAsync_When_User_Is_Not_Exist_Then_Should_Throw_Exception()
        {
            //arrange
            var email = "vl.kuzmich.st@gmail.com";
            var code = "sometoken";
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(null));

            //when then
            Assert.ThrowsAsync<ArgumentNullException>(() => underTest.ConfirmEmailAsync(email, code));
        }

        [Test]
        public async Task ConfirmEmailAsync_When_User_Exist_And_IdentityResult_Is_False_Then_Should_Return_False()
        {
            //arrange
            var operatinDetails = new OperationDetails(false, "Confirmation failed", "");
            var user = new ApplicationUser { Email = "vl.kuzmich.st@gmail.com" };
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            userManager
                .Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed("somefailed")));

            var actualResult = await underTest.ConfirmEmailAsync("vl.kuzmich.st@gmail.com", "sometoken");

            //when then
            Assert.AreEqual(actualResult.Succeeded, operatinDetails.Succeeded);
            Assert.AreEqual(actualResult.Message, operatinDetails.Message);
        }

        [Test]
        public async Task ConfirmEmailAsync_When_User_Exist_And_IdentityResult_Is_True_Then_Should_Return_True()
        {
            //arrange
            var operatinDetails = new OperationDetails(true, "Confirmation successful", "");
            var user = new ApplicationUser { Email = "vl.kuzmich.st@gmail.com" };
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            userManager
                .Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var actualResult = await underTest.ConfirmEmailAsync("vl.kuzmich.st@gmail.com", "sometoken");

            //when then
            Assert.AreEqual(actualResult.Succeeded, operatinDetails.Succeeded);
            Assert.AreEqual(actualResult.Message, operatinDetails.Message);
        }

        [Test]
        public void Authenticate_With_Null_User_Should_Thow_Exeptions()
        {
            //arrange
            UserDto userDto = null;
            //when then
            Assert.ThrowsAsync<ArgumentNullException>(() => underTest.Authenticate(userDto));
        }

        [Test]
        public async Task Authenticate_With_Non_Existing_User_Should_Return_TrueAsync()
        {
            //arrange
            var userDto = new UserDto { Email = "NotExist@test.com", Password = "12345678" };

            userManager
                .Setup(x => x.FindAsync(userDto.Email, userDto.Password))
                .Returns(Task.FromResult<ApplicationUser>(null));
            //when
            var actual = await underTest.Authenticate(userDto);
            //then
            Assert.AreEqual(actual, null);
        }

        [Test]
        public async Task Authenticate_With_Valid_credentials_Should_Return_TrueAsync()
        {
            //arrange
            var userDto = new UserDto { Email = "user@test.com", Password = "12345678" };

            ApplicationUser user = new ApplicationUser { Email = userDto.Email, PasswordHash = "123" };

            var tt = userManager
                .Setup(x => x.FindAsync(userDto.Email, userDto.Password))
                .ReturnsAsync(user);

            userManager
                .Setup(x => x.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie))
                .ReturnsAsync(new ClaimsIdentity());
            //when
            var actual = await underTest.Authenticate(userDto);
            //then
            Assert.True(actual.GetType() == typeof(ClaimsIdentity));
        }
    }
}
