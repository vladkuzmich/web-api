using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebAPI.Business.Contracts;
using WebAPI.Business.Contracts.Models.Dtos;
using WebAPI.Data.Contracts;

namespace WebAPI.Business.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly ICompanyConverter _companyConverter;

        public CompanyService(
            IUnitOfWork uow, 
            ILogger<CompanyService> logger, 
            ICompanyConverter companyConverter) 
            : base(uow, logger)
        {
            _companyConverter = companyConverter ?? throw new ArgumentNullException(nameof(companyConverter));
        }

        public Task CreateAsync(CompanyDto companyDto)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task EditAsync(CompanyDto companyDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}