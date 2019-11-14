using System.Collections.Generic;
using System.Linq;
using WebAPI.Business.Contracts;
using WebAPI.Business.Contracts.Models.Dtos;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Business.Converters
{
    public class CompanyConverter : ICompanyConverter
    {
        public CompanyDto ToCompanyDto(Company company) =>
            new CompanyDto
            {
                Id = company.Id,
                Name = company.Name
            };

        public IEnumerable<CompanyDto> ToCompanyDtos(IEnumerable<Company> companies) =>
            companies.Select(ToCompanyDto);

        public Company ToCompany(CompanyDto companyDto) =>
            new Company
            {
                Id = companyDto.Id,
                Name = companyDto.Name
            };

        public IEnumerable<Company> ToCompanies(IEnumerable<CompanyDto> companyDtos) =>
            companyDtos.Select(ToCompany);
    }
}