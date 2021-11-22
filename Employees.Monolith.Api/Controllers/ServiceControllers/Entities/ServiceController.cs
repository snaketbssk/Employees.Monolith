using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.Api.Controllers.UserControllers.Requests.Entities;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Managers.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.ServiceControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly DatabaseContext _context;
        public ServiceController(ILogger<ServiceController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS] PutSignUpAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(UserTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PutSignUpAsync([FromBody] SignUpRequest request)
        {
            var userManager = new UserManager(_context);
            var userTableCreater = request.ToUserTableCreater(RoleUserTable.Service);
            var result = await userManager.CreateAsync(userTableCreater, request.Referral, true, true);
            return Ok(result);
        }
    }
}
