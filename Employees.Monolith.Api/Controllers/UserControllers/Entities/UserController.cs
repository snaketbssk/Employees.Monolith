using Employees.Monolith.Api.Authentications.Models.Authentication.Entities;
using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.Api.Controllers.UserControllers.Requests.Entities;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Managers.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Employees.Monolith.LogicLayer.Models.Requests.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.UserControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly DatabaseContext _context;
        public UserController(ILogger<UserController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ALL] PutSignUpAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(UserTable), 200)]
        public async Task<IActionResult> PutSignUpAsync([FromBody] SignUpRequest request)
        {

            var userManager = new UserManager(_context);
            var userTableCreater = request.ToUserTableCreater(RoleUserTable.User);
            var result = await userManager.CreateAsync(userTableCreater, request.Referral, true, true);
            return Ok(result);
        }
        /// <summary>
        /// [IDENTITY] PathEditAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(UserTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.IDENTITY)]
        public async Task<IActionResult> PathEditAsync([FromBody] EditRequest request)
        {
            var userAuthentication = new UserAuthentication(HttpContext.User);

            var userTable = await _context.Users.FirstOrDefaultAsync(v => v.Id.Equals(userAuthentication.Id));
            userTable = request.Update(userTable);
            var result = _context.Users.Update(userTable);
            await _context.SaveChangesAsync();
            return Ok(result.Entity);
        }
        /// <summary>
        /// [IDENTITY] GetAsync
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.IDENTITY)]
        public async Task<IActionResult> GetAsync()
        {
            var userAuthentication = new UserAuthentication(HttpContext.User);
            var result = await _context.Users.FirstOrDefaultAsync(v => v.Id.Equals(userAuthentication.Id));
            return Ok(result);
        }
        /// <summary>
        /// [USER] DeleteAsync
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(UserTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.USER)]
        public async Task<IActionResult> DeleteAsync()
        {
            var userAuthentication = new UserAuthentication(HttpContext.User);
            var result = await _context.Users.FirstOrDefaultAsync(v => v.Id.Equals(userAuthentication.Id));
            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS] GetByGuidUserAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{Guid}")]
        [ProducesResponseType(typeof(UserTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetByGuidUserAsync([FromRoute] GuidRequest request)
        {
            var result = await _context.Users.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS] GetByGuidXTokenAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("Token/{Guid}")]
        [ProducesResponseType(typeof(UserTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetByGuidXTokenAsync([FromRoute] GuidRequest request)
        {
            var result = await _context.Tokens
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            return Ok(result.User);
        }
        /// <summary>
        /// [ADMINISTRATORS] DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("{Guid}")]
        [ProducesResponseType(typeof(UserTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> DeleteAsync([FromRoute] GuidRequest request)
        {
            var result = await _context.Users.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
