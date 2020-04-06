using System.Collections.Generic;
using WebAPI.API.Models.Documents;
using WebAPI.Business.Contracts.Models.Dtos;

namespace WebAPI.API.Converters.Interfaces
{
    public interface ICompanyDocumentConverter
    {
        CompanyDocument ToCompanyDocument(CompanyDto companyDto);
        IEnumerable<CompanyDocument> ToCompanyDocuments(IEnumerable<CompanyDto> companyDtos);
        CompanyDto ToCompanyDto(CompanyDocument companyDocument);
        IEnumerable<CompanyDto> ToCompanyDtos(IEnumerable<CompanyDocument> companyDocuments);
    }
}
