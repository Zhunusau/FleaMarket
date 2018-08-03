using GodelMastery.FleaMarket.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper;
using GodelMastery.FleaMarket.BL.Core.Helpers.LotComparer;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using NLog;
using GodelMastery.FleaMarket.BL.BusinessModels;
using GodelMastery.FleaMarket.BL.Core.Helpers.ExtentionsMethods;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class LotService : BaseService, ILotService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IHtmlParserProvider htmlParserProvider;
        private readonly ILotModelFactory lotModelFactory;
        private readonly IFilterModelFactory filterModelFactory;

        public LotService(
            IUnitOfWork unitOfWork, 
            IHtmlParserProvider htmlParserProvider,
            ILotModelFactory lotModelFactory,
            IFilterModelFactory filterModelFactory) : base(unitOfWork)
        {
            this.htmlParserProvider = htmlParserProvider ?? throw new ArgumentNullException(nameof(htmlParserProvider)); ;
            this.lotModelFactory = lotModelFactory ?? throw new ArgumentNullException(nameof(lotModelFactory)); ;
            this.filterModelFactory = filterModelFactory ?? throw new ArgumentNullException(nameof(filterModelFactory)); ;
        }

        public IEnumerable<LotDto> GetLotDtos(int filterId)
        {
            var filter = unitOfWork.Filters.GetById(filterId);
            if (filter == null)
            {
                throw new NullReferenceException(nameof(filter));
            }
            logger.Info($"Get a collection of lots by filter id {filterId}");
            return lotModelFactory.CreateLotDtos(filter.Lots);
        }

        public async Task<NewLotDtosModel> UpdateLots(int filterId)
        {
            try
            {
                logger.Info($"Operation UpdateLots for filter with id {filterId} was started");
                var currentFilter = unitOfWork.Filters.GetById(filterId);
                if (currentFilter != null)
                {
                    var currnetFilterDto = filterModelFactory.CreateFilterDto(currentFilter);

                    var lotDtosFromParser = await htmlParserProvider.GetLotsByFilter(currnetFilterDto.Content);

                    var lotsFromParser = lotModelFactory.CreateLots(lotDtosFromParser).ToList();
                    var lotsFromDB = GetLotsByFilterId(currentFilter.Id);

                    var newLots = GetNewLots(lotsFromDB, lotsFromParser);
                    var notActualLot = GetNotActualLots(lotsFromDB, lotsFromParser);
                    var toUpdateLots = GetToUpdateLots(lotsFromDB, lotsFromParser, newLots);

                    AddNewLots(newLots, currentFilter.Id);
                    UpdatePrice(toUpdateLots, currentFilter.Id);
                    DeleteNotActualLots(notActualLot, currentFilter.Id);

                    await unitOfWork.SaveChanges();

                    var newLotDtosModel = lotModelFactory.CreateNewLotDtosModel(currnetFilterDto, newLots);

                    return newLotDtosModel;
                }
            }
            catch(Exception e)
            {
                unitOfWork.RollBack();
                logger.Error($"Exception occurred during operation UpdateLots for filter with id: {filterId}. Error message: {e.Message}");
            }
            return null;
        }

        private List<Lot> GetNewLots(List<Lot> lotsFromDB, List<Lot> lotsFromParser)
        {
            var newLots = lotsFromParser.Except<Lot, LotsComparerToAddNew>(lotsFromDB);
            return newLots.ToList();
        }

        private List<Lot> GetNotActualLots(List<Lot> lotsFromDB, List<Lot> lotsFromParser)
        {
            var notActualLots = lotsFromDB.Except<Lot, LotsComparerToAddNew>(lotsFromParser);
            return notActualLots.ToList();
        }

        private List<Lot> GetToUpdateLots(List<Lot> lotsFromDB, List<Lot> lotsFromParser, List<Lot> newLots)
        { 
            var toUpdateWithNewLots = lotsFromParser.Except<Lot, LotsComparerToUpdate>(lotsFromDB);
            var toUpdateLots = toUpdateWithNewLots.Except<Lot, LotsComparerToAddNew>(newLots);
            return toUpdateLots.ToList();
        }

        private void AddNewLots(List<Lot> newLots, int currentFilterId)
        {
            foreach (var lot in newLots)
            {
                logger.Info($"Add new lot with SourceId: {lot.SourceId}");
                lot.DateOfFound = DateTime.Now;
                lot.FilterId = currentFilterId;
                unitOfWork.Lots.Create(lot);
            }
        }

        private void UpdatePrice(List<Lot> toUpdateLots, int filterId)
        {
            foreach (var lot in toUpdateLots)
            {
                logger.Info($"Update lot {lot.Id}");
                var lotFromDb = unitOfWork.Lots.SingleOrDefault(x => x.SourceId == lot.SourceId);
                lotFromDb.DateOfUpdate = DateTime.Now.ToString("F");
                lotFromDb.Price = lot.Price;
                unitOfWork.Lots.Update(lotFromDb);
            }
        }

        private void DeleteNotActualLots(List<Lot> notActualLot, int filterId)
        {
            foreach (var lot in notActualLot)
            {
                logger.Info($"Delete lot {lot.Id}");
                var lotFromDb = unitOfWork.Lots.GetById(lot.Id);
                unitOfWork.Lots.Delete(lotFromDb);
            }
        }

        private List<Lot> GetLotsByFilterId(int filterId)
        {
            var lotsFromDB = unitOfWork.Lots.Where(x => x.FilterId == filterId).ToList();
            return lotsFromDB;
        }
    }
}