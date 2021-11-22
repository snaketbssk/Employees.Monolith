using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.Api.Controllers.DepartmentControllers.Requests.Entities;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Employees.Monolith.LogicLayer.Models.Requests.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.DepartmentControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly DatabaseContext _context;
        public DepartmentController(ILogger<DepartmentController> logger, DatabaseContext context)
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
        [ProducesResponseType(typeof(DepartmentTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PutAsync([FromBody] PutDepartmentRequest request)
        {
            var department = request.Create();
            var result = await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return Ok(result.Entity);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{Guid}")]
        [ProducesResponseType(typeof(DepartmentTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] GuidRequest request)
        {
            var result = await _context.Departments.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="guidRequest"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("{Guid}")]
        [ProducesResponseType(typeof(DepartmentTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PatchAsync([FromRoute] GuidRequest guidRequest, [FromBody] PutDepartmentRequest request)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(v => v.Guid.Equals(guidRequest.Guid));
            department = request.Update(department);
            var result = _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return Ok(result.Entity);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("{Guid}")]
        [ProducesResponseType(typeof(DepartmentTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> DeleteAsync([FromRoute] GuidRequest request)
        {
            var result = await _context.Departments.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            _context.Departments.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
