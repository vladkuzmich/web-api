using System;
using Microsoft.Extensions.Logging;
using WebAPI.Data.Contracts;

namespace WebAPI.Business.Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork Uow;
        protected readonly ILogger<BaseService> Logger;

        protected BaseService(IUnitOfWork uow, ILogger<BaseService> logger)
        {
            Uow = uow ?? throw new ArgumentNullException(nameof(uow));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}