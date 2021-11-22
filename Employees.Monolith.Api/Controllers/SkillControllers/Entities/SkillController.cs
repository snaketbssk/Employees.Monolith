using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.Api.Controllers.DepartmentControllers.Requests.Entities;
using Employees.Monolith.Api.Controllers.SkillControllers.Requests.Entities;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Employees.Monolith.LogicLayer.Models.Requests.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.SkillControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class SkillController : ControllerBase
    {
        private readonly ILogger<SkillController> _logger;
        private readonly DatabaseContext _context;
        public SkillController(ILogger<SkillController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(SkillTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PutAsync([FromBody] PutSkillRequest request)
        {
            var skill = request.Create();
            var result = await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
            return Ok(result.Entity);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{Guid}")]
        [ProducesResponseType(typeof(SkillTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] GuidRequest request)
        {
            var result = await _context.Skills.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="guidRequest"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("{Guid}")]
        [ProducesResponseType(typeof(SkillTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PatchAsync([FromRoute] GuidRequest guidRequest, [FromBody] PutSkillRequest request)
        {
            var skill = await _context.Skills.FirstOrDefaultAsync(v => v.Guid.Equals(guidRequest.Guid));
            skill = request.Update(skill);
            var result = _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            return Ok(result.Entity);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("{Guid}")]
        [ProducesResponseType(typeof(SkillTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> DeleteAsync([FromRoute] GuidRequest request)
        {
            var result = await _context.Skills.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            _context.Skills.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
