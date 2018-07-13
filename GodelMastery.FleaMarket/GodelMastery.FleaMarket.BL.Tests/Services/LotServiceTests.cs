using System;
using System.Collections.Generic;
using System.Linq;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Services;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Moq;
using NUnit.Framework;
using GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.BusinessModels;

namespace GodelMastery.FleaMarket.BL.Tests.Services
{
    public class LotServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<ILotModelFactory> lotModelFactory;
        private Mock<IHtmlParserProvider> htmlParserProvider;
        private Mock<IFilterModelFactory> filterModelFactory;
        private LotService underTest;

        [SetUp]
        public void Init()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            lotModelFactory = new Mock<ILotModelFactory>();
            htmlParserProvider = new Mock<IHtmlParserProvider>();
            filterModelFactory = new Mock<IFilterModelFactory>();
            underTest = new LotService(
                unitOfWork.Object, 
                htmlParserProvider.Object, 
                lotModelFactory.Object, 
                filterModelFactory.Object);
        }

        [Test]
        public void GetLotDtos_When_Filter_Is_Not_Exist_Then_Should_Throw_Exception()
        {
            //arrange
            int incorrectId = -4;
            unitOfWork
                .Setup(x => x.Filters.GetById(It.IsAny<int>()))
                .Returns<Filter>(null);

            //act assert
            Assert.Throws<NullReferenceException>(() => underTest.GetLotDtos(incorrectId));
        }

        [Test]
        public void GetLotDtos_When_Filter_Is_Exist_Then_Should_Return_LotDtos()
        {
            //arrange
            var filter = new Filter
            {
                Id = 1,
                Content = "SomeContent",
                FilterName = "SomeName",
                Lots = new List<Lot> { new Lot { Name = "somelotname", Location = "someCity" } }
            };
            var expectedResult = new List<LotDto> { new LotDto { Name = "somelotname", Location = "someCity" } };
            unitOfWork
                .Setup(x => x.Filters.GetById(1))
                .Returns(filter);
            lotModelFactory
                .Setup(x => x.CreateLotDtos(filter.Lots))
                .Returns(expectedResult);

            //act
            var actualResult = underTest.GetLotDtos(1);

            //assert
            Assert.AreEqual(expectedResult.First(), actualResult.First());
        }
        [Test]
        public async Task UpdateLots_When_Exception_Occure_Should_RollBack_Method_Call()
        {
            //arrange
            int id = 1;
            unitOfWork.Setup(x => x.Filters.GetById(id)).Throws<Exception>();

            //act
            await underTest.UpdateLots(id);

            //assert
            unitOfWork.Verify(x => x.RollBack());
        }
        [Test]
        public async Task UpdateLots_When_Filter_Not_Exist_Should_Return_Null()
        {
            //arrange
            int id = 1;
            Filter expectedResult = null;
                
            unitOfWork.Setup(x => x.Filters.GetById(id)).Returns<Filter>(null);

            //act
            var actualResult = await underTest.UpdateLots(id);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public async Task UpdateLots_When_Filter_Exist_Should_Return_NewLotDtosModel()
        {
            //arrange
            int id = 1;
            var currentFilter = new Filter { FilterName = "Filter Name", Content = "Filter Content" };
            var newLots = new List<Lot>();
            var newLotsDto = new List<LotDto>();
            FilterDto currentFilterDto = new FilterDto { FilterName = "Filter Name", Content = "Filter Content" };
            var expectedResult = new NewLotDtosModel {FilterDto = currentFilterDto, FreshLots = newLotsDto };

            unitOfWork.Setup(x => x.Filters.GetById(id)).Returns(currentFilter);
            filterModelFactory.Setup(x => x.CreateFilterDto(currentFilter)).Returns(currentFilterDto);
            htmlParserProvider.Setup(x => x.GetLotsByFilter(It.IsAny<String>()))
                .Returns(Task.FromResult(newLotsDto));
            unitOfWork.Setup(x => x.Lots.GetAll).Returns(newLots);
            lotModelFactory.Setup(x => x.CreateNewLotDtosModel(currentFilterDto, newLots)).Returns(expectedResult);

            //act
            var actualResult = await underTest.UpdateLots(id);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
            htmlParserProvider.Verify(x => x.GetLotsByFilter(currentFilter.Content));
            unitOfWork.Verify(x => x.SaveChanges());
        }
    }
}
