using System;
using System.Collections.Generic;
using System.Linq;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.BL.Services;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;

namespace GodelMastery.FleaMarket.BL.Tests.Services
{
    [TestFixture]
    public class FilterServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IFilterModelFactory> filterModelFactory;
        private Mock<IUserStore<ApplicationUser>> userStore;
        private Mock<UserManager<ApplicationUser>> userManager;
        private IFilterService underTest;

        [SetUp]
        public void Init()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            userStore = new Mock<IUserStore<ApplicationUser>>();
            userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object);
            unitOfWork.Setup(x => x.UserManager).Returns(userManager.Object);
            filterModelFactory = new Mock<IFilterModelFactory>();
            underTest = new FilterService(unitOfWork.Object, filterModelFactory.Object);
        }

        [Test]
        public void GetUserFilters_When_User_Is_Not_Exist_Then_Should_Throw_Exception()
        {
            //arrange
            var email = "test@gmail.com";
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(null));

            //when then
            Assert.ThrowsAsync<ArgumentNullException>(() => underTest.GetFilterDtos(email));
        }

        [Test]
        public async Task GetUserFilters_When_User_Is_Exist_Then_Should_Return_FilterDtos()
        {
            //arrange
            var filterDtos = new List<FilterDto> {new FilterDto { ApplicationUserId = "appId", FilterName = "Iphone", Content = "Iphone 6s" }};
            var filters = new List<Filter> { new Filter { ApplicationUserId = "appId", FilterName = "Iphone", Content = "Iphone 6s" } };
            var applicationUser = new ApplicationUser {Id = "appId", Email = "test@gmail.com", Filters = filters };
            var email = "test@gmail.com";
            userManager
                .Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(applicationUser);
            filterModelFactory
                .Setup(x => x.CreateFilterDtos(filters))
                .Returns(filterDtos);

            //when 
            var actualResult = await underTest.GetFilterDtos(email);

            Assert.AreEqual(actualResult.First(), filterDtos.First());
        }
    }
}
