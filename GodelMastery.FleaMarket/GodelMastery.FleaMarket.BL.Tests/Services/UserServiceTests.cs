using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Services;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.Helpers.SchedulerHelper;

namespace GodelMastery.FleaMarket.BL.Tests.Services
{
    class UserServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IUserModelFactory> userModelFactory;
        private Mock<IUserStore<ApplicationUser>> userStore;
        private Mock<UserManager<ApplicationUser>> userManager;
        private Mock<ISchedulerManager> schedulerManager;
        private UserService underTest;
        [SetUp]
        public void Init()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            userStore = new Mock<IUserStore<ApplicationUser>>();
            userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object);
            unitOfWork.Setup(x => x.UserManager).Returns(userManager.Object);
            userModelFactory = new Mock<IUserModelFactory>();
            underTest = new UserService(unitOfWork.Object, userModelFactory.Object);
        }

        [Test]
        public void GetUserInfo_When_User_Is_Not_Exist_Then_Should_Throw_Exception()
        {
            //arrange
            var email = "test@gmail.com";
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(null));
            //when then
            Assert.ThrowsAsync<ArgumentNullException>(() => underTest.GetUserDto(email));
        }
        [Test]
        public async Task GetUserInfo_When_User_Is_Exist_Then_Should_Return_UserDto()
        {
            //arrange
            var applicationUser = new ApplicationUser { Email = "vl.kuzmich.st@gmail.com", FirstName = "Vlad", LastName = "Kuzmich" };
            var userDto = new UserDto { Email = "vl.kuzmich.st@gmail.com", FirstName = "Vlad", LastName = "Kuzmich" };
            var email = "vl.kuzmich.st@gmail.com";
            userManager
                .Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(applicationUser);
            userModelFactory
                .Setup(x => x.CreateUserDto(applicationUser))
                .Returns(userDto);
            //when
            var actualResult = await underTest.GetUserDto(email);
            //then
            Assert.AreEqual(actualResult, userDto);
        }
    }
}
