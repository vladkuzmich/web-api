using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.API.Converters.Interfaces;
using WebAPI.API.Documents;
using WebAPI.Business.Contracts;

namespace WebAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserDocumentConverter _userDocumentConverter;

        public UsersController(IUserService userService, IUserDocumentConverter userDocumentConverter)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userDocumentConverter = userDocumentConverter ?? throw new ArgumentNullException(nameof(userDocumentConverter));
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

        // GET: api/users
        [HttpGet]
        public async Task<IEnumerable<UserDocument>> GetAll() 
            => _userDocumentConverter.ToUserDocuments(await _userService.GetAllAsync());

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserDocument>> Create([FromBody]UserDocument user)
        {
            await _userService.CreateAsync(_userDocumentConverter.ToUserDto(user));

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody]UserDocument user)
        {
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

            await _userService.DeleteAsync(userDto);

            return NoContent();
        }

        // PUT: api/users/company
        [HttpPut]
        [Route("api/users/{id}/company")]
        public async Task<ActionResult> ChangeUserCompany(int id)
        {

        }
    }
}
