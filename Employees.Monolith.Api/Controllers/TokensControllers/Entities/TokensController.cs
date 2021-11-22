using Employees.Monolith.Api.Authentications.Models.Authentication.Entities;
using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Employees.Monolith.LogicLayer.Models.Requests.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.XTokensControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class TokensController : ControllerBase
    {
        private readonly ILogger<TokensController> _logger;
        private readonly DatabaseContext _context;
        public TokensController(ILogger<TokensController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [IDENTITY] GetAsync
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TokenTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.IDENTITY)]
        public async Task<IActionResult> GetAsync()
        {
            var userAuthentication = new UserAuthentication(HttpContext.User);
            var result = await _context.Tokens.Where(v => v.UserId.Equals(userAuthentication.Id)).ToListAsync();
            return Ok(result);
        }
        /// <summary>
        /// [HUMAN] DeleteAsync
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(IEnumerable<TokenTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.HUMAN)]
        public async Task<IActionResult> DeleteAsync()
        {
            var userAuthentication = new UserAuthentication(HttpContext.User);
            var result = await _context.Tokens.Where(v => v.UserId.Equals(userAuthentication.Id)).ToListAsync();
            _context.Tokens.RemoveRange(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS] GetAsync
        /// </summary>
        /// <returns></returns>
        [HttpGet("User/{Guid}")]
        [ProducesResponseType(typeof(IEnumerable<TokenTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] GuidRequest request)
        {
            var userTable = await _context.Users.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            var results = await _context.Tokens.Where(v => v.UserId.Equals(userTable.Id)).ToListAsync();
            return Ok(results);
        }
        /// <summary>
        /// [ADMINISTRATORS] GetAsync
        /// </summary>
        /// <returns></returns>
        [HttpDelete("User/{Guid}")]
        [ProducesResponseType(typeof(IEnumerable<TokenTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> DeleteAsync([FromRoute] GuidRequest request)
        {
            var userTable = await _context.Users.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            var results = await _context.Tokens.Where(v => v.UserId.Equals(userTable.Id)).ToListAsync();
            _context.Tokens.RemoveRange(results);
            await _context.SaveChangesAsync();
            return Ok(results);
        }
    }
}
