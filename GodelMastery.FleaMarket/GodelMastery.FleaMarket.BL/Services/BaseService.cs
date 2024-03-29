﻿using GodelMastery.FleaMarket.DAL.Interfaces;
using System;

namespace GodelMastery.FleaMarket.BL.Services
{
    public abstract class BaseService
    {
        protected IUnitOfWork unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
    }
}
