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
            Assert.Throws<ArgumentNullException>(() => underTest.GetLotDtos(incorrectId));
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
    }
}
