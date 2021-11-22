using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.Api.Controllers.DepartmentControllers.Requests.Entities;
using Employees.Monolith.Api.Controllers.SkillsControllers.Requests.Entities;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Employees.Monolith.LogicLayer.Models.Requests.Entities;
using Employees.Monolith.LogicLayer.Models.Responses.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.SkillsControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class SkillsController : ControllerBase
    {
        private readonly ILogger<SkillsController> _logger;
        private readonly DatabaseContext _context;
        public SkillsController(ILogger<SkillsController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SkillTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _context.Skills.ToListAsync();
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="paginationRequest"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{From}/{To}")]
        [ProducesResponseType(typeof(IEnumerable<SkillTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PostAsync([FromRoute] PaginationRequest paginationRequest, [FromBody] PostKillsRequest request)
        {
            IQueryable<SkillTable> query = _context.Skills;
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(v => v.Title.Contains(request.Title));
            }
            var take = paginationRequest.GetTake();
            var totalCount = await query.CountAsync();
            var skills = await query
                .OrderBy(v => v.Id)
                .Skip(paginationRequest.From)
                .Take(take)
                .ToListAsync();
            var result = new PaginationResponse<SkillTable>(totalCount);
            result.Values.AddRange(skills);
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{From}/{To}")]
        [ProducesResponseType(typeof(PaginationResponse<SkillTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] PaginationRequest request)
        {
            var take = request.GetTake();
            var totalCount = await _context.Skills.CountAsync();
            var employees = await _context.Skills
                .OrderBy(v => v.Id)
                .Skip(request.From)
                .Take(take)
                .ToListAsync();
            var result = new PaginationResponse<SkillTable>(totalCount);
            result.Values.AddRange(employees);
            return Ok(result);
        }
    }
}
