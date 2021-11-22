using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.BotsControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class BotsController : ControllerBase
    {
        private readonly ILogger<BotsController> _logger;
        private readonly DatabaseContext _context;
        public BotsController(ILogger<BotsController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS] GetAsync
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _context.Users.Where(v => v.Role.Equals(RoleUserTable.Bot)).ToListAsync();
            return Ok(result);
        }
    }
}
