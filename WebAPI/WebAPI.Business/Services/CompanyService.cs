using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUserConverter _userConverter;

        public CompanyService(
            IUnitOfWork uow, 
            ILogger<CompanyService> logger, 
            ICompanyConverter companyConverter,
            IUserConverter userConverter) 
            : base(uow, logger)
        {
            _companyConverter = companyConverter ?? throw new ArgumentNullException(nameof(companyConverter));
            _userConverter = userConverter ?? throw new ArgumentNullException(nameof(userConverter));
        }

        public async Task<CompanyDto> GetByIdAsync(int id)
        {
            var company = await Uow.Companies.GetByIdAsync(id);

            return company == null
                ? null
                : _companyConverter.ToCompanyDto(company);
        }

        public async Task<IEnumerable<CompanyDto>> GetAllAsync() =>
            _companyConverter.ToCompanyDtos(await Uow.Companies.GetAllAsync());

        public async Task<CompanyDto> CreateAsync(CompanyDto companyDto)
        {
            ThrowIfNull(companyDto);

            try
            {
                var company = _companyConverter.ToCompany(companyDto);
                Uow.Companies.Create(company);
                await Uow.CommitAsync();

                return _companyConverter.ToCompanyDto(company);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error while creating company");
                throw;
            }
        }

        public async Task EditAsync(int id, CompanyDto companyDto)
        {
            ThrowIfNull(companyDto);

            var company = await Uow.Companies.GetByIdAsync(id);
            if (company == null)
            {
                throw new ArgumentException($"Company with id: '{id}' not found", nameof(id));
            }

            try
            {
                company.Name = companyDto.Name;

                Uow.Companies.Edit(company);
                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while editing company with id: '{id}'");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var company = await Uow.Companies.GetByIdAsync(id);

            if (company == null)
            {
                throw new ArgumentException($"Company with id: '{id}' not found", nameof(id));
            }

            try
            {
                Uow.Companies.Delete(company);

                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while deleting company with id: '{id}'");
                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetUsersByCompanyId(int id)
        {
            var company = await Uow.Companies.GetByIdAsync(id);

            if (company == null)
            {
                throw new ArgumentException($"Could not found company with id: '{id}'", nameof(id));
            }

            var users = await Uow.Users.WhereAsync(x => x.CompanyId == id);

            return users.Any()
                ? _userConverter.ToUserDtos(users)
                : new List<UserDto>();
        }

        private void ThrowIfNull(CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                throw new ArgumentNullException(nameof(companyDto));
            }
        }
    }
}