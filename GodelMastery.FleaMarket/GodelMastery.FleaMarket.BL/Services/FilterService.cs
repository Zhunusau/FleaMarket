﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Core.Helpers;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.DAL.Interfaces;
using NLog;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class FilterService : BaseService, IFilterService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IFilterModelFactory filterModelFactory;

        public FilterService(IUnitOfWork unitOfWork, IFilterModelFactory filterModelFactory) : base(unitOfWork)
        {
            this.filterModelFactory = filterModelFactory ?? throw new ArgumentNullException(nameof(filterModelFactory));
        }

        public async Task<IEnumerable<FilterDto>> GetFilterDtos(string login)
        {
            var applicationUser = await unitOfWork.UserManager.FindByEmailAsync(login);
            if (applicationUser == null)
            {
                throw new NullReferenceException(nameof(applicationUser));
            }
            logger.Info($"Get a collection of filters by user login {login}");
            return filterModelFactory.CreateFilterDtos(applicationUser.Filters);
        }

        public FilterDto GetFilterById(int filterId)
        {
            var filter = unitOfWork.Filters.GetById(filterId);
            if (filter == null)
            {
                throw new NullReferenceException(nameof(filter));
            }
            logger.Info($"Get filter with filter id {filterId}");
            return filterModelFactory.CreateFilterDto(filter);
        }

        public async Task<OperationDetails> Create(FilterDto filterDto)
        {
            logger.Info($"Create filter {filterDto.FilterName}");
            try
            {
                var filter = unitOfWork.Filters.SingleOrDefault(f =>
                    f.ApplicationUserId.Equals(filterDto.ApplicationUserId) && f.FilterName.Equals(filterDto.FilterName));
                if (filter != null)
                {
                    logger.Error($"User already have a filter with name \"{filterDto.FilterName}\"");
                    return new OperationDetails(false, $"You already have a filter with name \"{filterDto.FilterName}\"", "");
                }
                filter = filterModelFactory.CreateFilter(filterDto);
                unitOfWork.Filters.Create(filter);
                await unitOfWork.SaveChanges();
                logger.Info($"Filter \"{filterDto.FilterName}\" was created successfully");
                return new OperationDetails(true, $"Filter \"{filterDto.FilterName}\" was created successfully", "");
            }
            catch (DataException e)
            {
                unitOfWork.RollBack();
                logger.Error(e.Message);
                throw new DataException(e.Message);
            }
        }
        public async Task RemoveFilter(int id)
        {
            var filter = unitOfWork.Filters.GetById(id);
            if (filter == null)
            {
                throw new NullReferenceException(nameof(filter));
            }

            logger.Info($"Remove filter with id: {id}");
            try
            {
                unitOfWork.Filters.Delete(filter);
                await unitOfWork.SaveChanges();
            }
            catch(Exception e)
            {
                logger.Error($"Filter with {id} can't be removed due to exception: {e.Message}");
                unitOfWork.RollBack();
            }
        }
    }
}
