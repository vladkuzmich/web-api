using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.API.Converters.Interfaces;
using WebAPI.API.Models.Documents;
using WebAPI.Business.Contracts;

namespace WebAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyDocumentConverter _companyDocumentConverter;
        private readonly IUserDocumentConverter _userDocumentConverter;

        public CompaniesController(
            ICompanyService companyService, 
            ICompanyDocumentConverter companyDocumentConverter,
            IUserDocumentConverter userDocumentConverter)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _companyDocumentConverter = companyDocumentConverter ?? throw new ArgumentNullException(nameof(companyDocumentConverter));
            _userDocumentConverter = userDocumentConverter ?? throw new ArgumentNullException(nameof(userDocumentConverter));
        }

        // GET: api/companies
        [HttpGet]
        public async Task<IEnumerable<CompanyDocument>> GetAll() =>
            _companyDocumentConverter.ToCompanyDocuments(await _companyService.GetAllAsync());

        // GET: api/companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDocument>> GetById(int id)
        {
            var companyDto = await _companyService.GetByIdAsync(id);
            if (companyDto == null)
            {
                return NotFound();
            }

            return _companyDocumentConverter.ToCompanyDocument(companyDto);
        }
        
        // POST: api/companies
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CompanyDocument company)
        {
            var createdCompany = await _companyService.CreateAsync(_companyDocumentConverter.ToCompanyDto(company));

            return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, createdCompany);
        }

        // PUT: api/companies/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody]CompanyDocument company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            var companyDto = await _companyService.GetByIdAsync(id);
            if (companyDto == null)
            {
                return NotFound();
            }

            await _companyService.EditAsync(id, _companyDocumentConverter.ToCompanyDto(company));

            return NoContent();
        }

        // DELETE: api/companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var companyDto = await _companyService.GetByIdAsync(id);
            if (companyDto == null)
            {
                return NotFound();
            }

            await _companyService.DeleteAsync(id);

            return NoContent();
        }

        // GET: api/companies/5/users
        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<UserDocument>>> GetUsersByCompany(int id)
        {
            var companyDto = await _companyService.GetByIdAsync(id);
            if (companyDto == null)
            {
                return NotFound($"Company with id: '{id}' not found");
            }

            var userDtos = await _companyService.GetUsersByCompanyId(id);

            return Ok(_userDocumentConverter.ToUserDocuments(userDtos));
        }
    }
}