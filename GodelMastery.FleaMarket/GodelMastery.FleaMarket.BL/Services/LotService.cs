using GodelMastery.FleaMarket.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper;
using GodelMastery.FleaMarket.BL.Core.Helpers.LotComparer;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using NLog;
using GodelMastery.FleaMarket.BL.BusinessModels;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class LotService : BaseService, ILotService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IHtmlParserProvider htmlParserProvider;
        private ILotModelFactory lotModelFactory;
        private IFilterModelFactory filterModelFactory;

        public LotService(
            IUnitOfWork unitOfWork, 
            IHtmlParserProvider htmlParserProvider,
            ILotModelFactory lotModelFactory,
            IFilterModelFactory filterModelFactory) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.htmlParserProvider = htmlParserProvider;
            this.lotModelFactory = lotModelFactory;
            this.filterModelFactory = filterModelFactory;
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
                logger.Info($"Start lots update filter ID: {filterId}");
                var currentFilter = unitOfWork.Filters.GetById(filterId);
                if (currentFilter != null)
                {
                    var lotsFromDB = currentFilter.Lots;

                    var currnetFilterDto = filterModelFactory.CreateFilterDto(currentFilter);

                    var lotsDtosFromParser = await htmlParserProvider.GetLotsByFilter(currnetFilterDto.Content);
                    var lotDtosFromDB = lotModelFactory.CreateLotDtos(lotsFromDB).ToList();

                    var newLots = GetNewLots(lotDtosFromDB, lotsDtosFromParser);
                    var notActualLot = GetNotActualLots(lotDtosFromDB, lotsDtosFromParser);
                    var toUpdateLots = GetToUpdateLots(lotDtosFromDB, lotsDtosFromParser, lotModelFactory.CreateLotDtos(newLots));

                    AddNewLots(newLots, currentFilter);
                    UpdatePrice(toUpdateLots, currentFilter);
                    DeleteNotActualLots(notActualLot, currentFilter);

                    await unitOfWork.SaveChanges();

                    var newLotDtosModel = lotModelFactory.CreateNewLotDtosModel(currnetFilterDto, newLots);

                    return newLotDtosModel;
                }
            }
            catch(Exception e)
            {
                unitOfWork.RollBack();
                logger.Error($"Update exception {e.Message}");
            }
            return null;
        }
        private IEnumerable<Lot> GetNewLots(IEnumerable<LotDto> lotDtosFromDB, IEnumerable<LotDto> lotsDtosFromParser)
        {
            var newLotDtos = lotsDtosFromParser.Except(lotDtosFromDB, new LotsComparerToAddNew());
            var newLots = lotModelFactory.CreateLots(newLotDtos);
            return newLots;
        }
        private IEnumerable<Lot> GetNotActualLots(IEnumerable<LotDto> lotDtosFromDB, IEnumerable<LotDto> lotsDtosFromParser)
        {
            var notActualLotDtos = lotDtosFromDB.Except(lotsDtosFromParser, new LotsComparerToAddNew());
            var notActualLots = lotModelFactory.CreateLots(notActualLotDtos);
            return notActualLots;
        }
        private IEnumerable<Lot> GetToUpdateLots(IEnumerable<LotDto> lotDtosFromDB, IEnumerable<LotDto> lotsDtosFromParser, IEnumerable<LotDto> newLotDtos)
        { 
            var toUpdateWithNewLots = lotsDtosFromParser.Except(lotDtosFromDB, new LotsComparerToUpdate());
            var toUpdateLots = toUpdateWithNewLots.Except(newLotDtos, new LotsComparerToAddNew());
            var notActualLots = lotModelFactory.CreateLots(toUpdateLots);
            return notActualLots;
        }
        private void AddNewLots(IEnumerable<Lot> newLots,Filter currentFilter)
        {
            foreach (var lot in newLots)
            {
                logger.Info($"Add new lot {lot.Id}");
                lot.DateOfFound = DateTime.Now;
                lot.FilterId = currentFilter.Id;
                unitOfWork.Lots.Create(lot);
            }
        }
        private void UpdatePrice(IEnumerable<Lot> toUpdateLots, Filter currentFilter)
        {
            foreach (var lot in toUpdateLots)
            {
                logger.Info($"Update lot {lot.Id}");
                var lotFromDb = currentFilter.Lots.Where(x => x.SourceId == lot.SourceId).FirstOrDefault();
                lotFromDb.DateOfUpdate = DateTime.Now;
                lotFromDb.Price = lot.Price;
                unitOfWork.Lots.Update(lotFromDb);
            }
        }
        private void DeleteNotActualLots(IEnumerable<Lot> notActualLot, Filter currentFilter)
        {
            foreach (var lot in notActualLot)
            {
                logger.Info($"Delete lot {lot.Id}");
                var lotFromDb = currentFilter.Lots.Where(x => x.SourceId == lot.SourceId).FirstOrDefault();
                unitOfWork.Lots.Delete(lotFromDb);
            }
        }
    }
}