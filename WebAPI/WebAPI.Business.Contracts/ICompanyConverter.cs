using System.Collections.Generic;
using WebAPI.Business.Contracts.Models.Dtos;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Business.Contracts
{
    public interface ICompanyConverter
    {
        CompanyDto ToCompanyDto(Company company);
        IEnumerable<CompanyDto> ToCompanyDtos(IEnumerable<Company> companies);
        Company ToCompany(CompanyDto companyDto);
        IEnumerable<Company> ToCompanies(IEnumerable<CompanyDto> companyDtos);
    }
}