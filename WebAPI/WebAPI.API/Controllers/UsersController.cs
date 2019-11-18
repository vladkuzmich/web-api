using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.API.Converters.Interfaces;
using WebAPI.API.Models.Documents;
using WebAPI.Business.Contracts;

namespace WebAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserDocumentConverter _userDocumentConverter;

        public UsersController(
            IUserService userService,
            ICompanyService companyService,
            IWebHostEnvironment webHostEnvironment,
            IUserDocumentConverter userDocumentConverter)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _userDocumentConverter = userDocumentConverter ?? throw new ArgumentNullException(nameof(userDocumentConverter));
        }

        // GET: api/users
        [HttpGet]
        public async Task<IEnumerable<UserDocument>> GetAll()
        {
            var userDtos = await _userService.GetAllAsync();

            return _userDocumentConverter.ToUserDocuments(userDtos);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDocument>> GetById(int id)
        {
            var userDto = await _userService.GetByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            return _userDocumentConverter.ToUserDocument(userDto);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserDocument>> Create([FromBody]UserDocument user)
        {
            var createdUser = await _userService.CreateAsync(_userDocumentConverter.ToUserDto(user));

            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody]UserDocument user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var userDto = await _userService.GetByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            await _userService.EditAsync(id, _userDocumentConverter.ToUserDto(user));

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userDto = await _userService.GetByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            await _userService.DeleteAsync(id);

            return NoContent();
        }

        // PUT: api/users/5/company/2
        [HttpPut("{userId}/company/{companyId}")]
        public async Task<ActionResult> ChangeCompany(int userId, int companyId)
        {
            var userDto = await _userService.GetByIdAsync(userId);
            if (userDto == null)
            {
                return NotFound($"User with id: {userId} not found");
            }

            var companyDto = await _companyService.GetByIdAsync(companyId);
            if (companyDto == null)
            {
                return NotFound($"Company with id: {companyId} not found");
            }

            await _userService.ChangeCompanyAsync(userId, companyId);

            return NoContent();
        }

        // POST: api/users/5/upload/url
        [HttpPost("{id}/upload")]
        public async Task<ActionResult> Upload(int id, IFormFile file)
        {
            var userDto = await _userService.GetByIdAsync(id);
            if (userDto == null)
            {
                return NotFound($"User with id: {id} not found");
            }

            if (file == null)
            {
                return BadRequest();
            }

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var storagePath = _webHostEnvironment.WebRootPath + "\\images\\";

            await _userService.UploadPhotoAsync(id, file.FileName, memoryStream.ToArray(), storagePath, 300, 300);

            return Ok();
        }
    }
}
