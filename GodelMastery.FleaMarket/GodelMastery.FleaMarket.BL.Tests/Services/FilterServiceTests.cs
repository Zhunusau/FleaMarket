using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.Services;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Core.Helpers;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;

namespace GodelMastery.FleaMarket.BL.Tests.Services
{
    [TestFixture]
    public class FilterServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IFilterModelFactory> filterModelFactory;
        private Mock<IUserStore<ApplicationUser>> userStore;
        private Mock<UserManager<ApplicationUser>> userManager;
        private FilterService underTest;
        private FilterDto filterDto;
        private ApplicationUser applicationUser;
        private Filter filter;

        [SetUp]
        public void Init()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            userStore = new Mock<IUserStore<ApplicationUser>>();
            userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object);
            unitOfWork.Setup(x => x.UserManager).Returns(userManager.Object);
            filterModelFactory = new Mock<IFilterModelFactory>();
            underTest = new FilterService(unitOfWork.Object, filterModelFactory.Object);
            filterDto = new FilterDto { FilterName = "Trek", Content = "Bicycle Trek 29", ApplicationUserId = "id" };
            applicationUser = new ApplicationUser { Id = "id", Email = "test@gmail.com", UserName = "test@gmail.com" };
            filter = new Filter
            {
                FilterName = filterDto.FilterName,
                Content = filterDto.Content,
                ApplicationUserId = applicationUser.Id
            };
        }

        [Test]
        public void GetUserFilters_When_User_Is_Not_Exist_Then_Should_Throw_Exception()
        {
            //arrange
            var email = "test@gmail.com";
            userManager
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(null));

            //act assert
            Assert.ThrowsAsync<ArgumentNullException>(() => underTest.GetFilterDtos(email));
        }

        [Test]
        public async Task GetUserFilters_When_User_Is_Exist_Then_Should_Return_FilterDtos()
        {
            //arrange
            var expectedResult = new List<FilterDto> {new FilterDto { ApplicationUserId = "appId", FilterName = "Iphone", Content = "Iphone 6s" }};
            var filters = new List<Filter> { new Filter { ApplicationUserId = "appId", FilterName = "Iphone", Content = "Iphone 6s" } };
            applicationUser = new ApplicationUser {Id = "appId", Email = "test@gmail.com", Filters = filters };
            var email = "test@gmail.com";
            userManager
                .Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(applicationUser);
            filterModelFactory
                .Setup(x => x.CreateFilterDtos(filters))
                .Returns(expectedResult);

            //act
            var actualResult = await underTest.GetFilterDtos(email);

            //assert
            Assert.AreEqual(expectedResult.First(), actualResult.First());
        }

        [Test]
        public void GetFilterById_When_Filter_Is_Not_Exist_Then_Should_Throw_Exception()
        {
            //arrange
            int incorrectId = -4;
            unitOfWork
                .Setup(x => x.Filters.GetById(It.IsAny<int>()))
                .Returns<Filter>(null);

            //act assert
            Assert.Throws<ArgumentNullException>(() => underTest.GetFilterById(incorrectId));
        }

        [Test]
        public async Task Create_When_Filter_Already_Exist_Then_Operation_Details_False()
        {
            //arrange
            var expectedResult = new OperationDetails(false, $"You already have a filter with name \"{filterDto.FilterName}\"", "");

            unitOfWork
                .Setup(u => u.Filters.SingleOrDefault(It.Is<Func<Filter, bool>>(i => i(filter))))
                .Returns(filter);

            //act
            var actualResult = await underTest.Create(filterDto);

            //assert
            Assert.AreEqual(expectedResult.Succeeded, actualResult.Succeeded);
        }

        [Test]
        public void GetFilterById_When_Filter_Is_Exist_Then_Should_Return_FilterDto()
        {
            //arrange
            var filter = new Filter {Id = 1, FilterName = "FilterName", Content = "FilterContent"};
            var expectedResult = new FilterDto { Id = 1, FilterName = "FilterName", Content = "FilterContent" };
            unitOfWork
                .Setup(x => x.Filters.GetById(1))
                .Returns(filter);
            filterModelFactory
                .Setup(x => x.CreateFilterDto(filter))
                .Returns(filterDto);

            //act
            var actualResult = underTest.GetFilterById(1);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Create_When_Filter_Added_Successfully_Then_Operation_Details_True()
        {
            //arrange
            var expectedResult = new OperationDetails(true, $"Filter \"{filterDto.FilterName}\" was created successfully", "");

            unitOfWork
                .Setup(u => u.Filters.SingleOrDefault(It.IsAny<Func<Filter, bool>>()))
                .Returns<Filter>(null);

            filterModelFactory
                .Setup(f => f.CreateFilter(filterDto))
                .Returns(filter);

            unitOfWork
                .Setup(u => u.Filters.Create(filter))
                .Verifiable();

            unitOfWork
                .Setup(u => u.SaveChanges())
                .Returns(() => Task.Run(() => { }))
                .Verifiable();

            //act
            var actualResult = await underTest.Create(filterDto);

            //assert
            Assert.AreEqual(expectedResult.Succeeded, actualResult.Succeeded);
        }

        [Test]
        public void Create_When_Filter_Added_With_Exception_Then_Operation_Throw_Exception()
        {
            //arrange

            unitOfWork
                .Setup(u => u.Filters.SingleOrDefault(It.IsAny<Func<Filter, bool>>()))
                .Returns<Filter>(null);

            filterModelFactory
                .Setup(f => f.CreateFilter(filterDto))
                .Returns(filter);

            unitOfWork
                .Setup(u => u.Filters.Create(filter))
                .Verifiable();

            unitOfWork
                .Setup(u => u.SaveChanges())
                .Throws(new DataException())
                .Verifiable();

            unitOfWork
                .Setup(u => u.RollBack())
                .Verifiable();

            //act assert
            Assert.ThrowsAsync<DataException>(() => underTest.Create(filterDto));
        }
    }
}
