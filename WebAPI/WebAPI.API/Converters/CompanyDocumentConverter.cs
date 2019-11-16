using System.Collections.Generic;
using System.Linq;
using WebAPI.API.Converters.Interfaces;
using WebAPI.API.Models.Documents;
using WebAPI.Business.Contracts.Models.Dtos;

namespace WebAPI.API.Converters
{
    public class CompanyDocumentConverter : ICompanyDocumentConverter
    {
        public CompanyDocument ToCompanyDocument(CompanyDto companyDto) =>
            new CompanyDocument
            {
                Id = companyDto.Id,
                Name = companyDto.Name
            };

        public IEnumerable<CompanyDocument> ToCompanyDocuments(IEnumerable<CompanyDto> companyDtos) =>
            companyDtos.Select(ToCompanyDocument);

        public CompanyDto ToCompanyDto(CompanyDocument companyDocument) =>
            new CompanyDto
            {
                Id = companyDocument.Id,
                Name = companyDocument.Name
            };

        public IEnumerable<CompanyDto> ToCompanyDtos(IEnumerable<CompanyDocument> companyDocuments) =>
            companyDocuments.Select(ToCompanyDto);
    }
}