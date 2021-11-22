using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.ReferralsControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class ReferralsController : ControllerBase
    {
        private readonly ILogger<ReferralsController> _logger;
        private readonly DatabaseContext _context;
        public ReferralsController(ILogger<ReferralsController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [IDENTITY] GetAsync
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.IDENTITY)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _context.Referrals
                .Include(v => v.Users)
                .ToListAsync();
            return Ok(result);
        }
    }
}
