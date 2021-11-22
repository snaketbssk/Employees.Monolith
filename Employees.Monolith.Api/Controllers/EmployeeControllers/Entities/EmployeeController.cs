using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.Api.Controllers.EmployeeControllers.Requests.Entities;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Managers.Entities;
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

namespace Employees.Monolith.Api.Controllers.EmployeeControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly DatabaseContext _context;
        public EmployeeController(ILogger<EmployeeController> logger, DatabaseContext context)
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
        [ProducesResponseType(typeof(EmployeeTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PutAsync([FromBody] PutEmployeeRequest request)
        {
            var employeeManager = new EmployeeManager(_context);
            var result = await employeeManager.CreateAsync(request);
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{Guid}")]
        [ProducesResponseType(typeof(EmployeeResponse), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] GuidRequest request)
        {
            var employee = await _context.Employees
                .Include(v => v.Department)
                .Include(v => v.Skills)
                .FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            var result = new EmployeeResponse(employee, employee.Department.Guid.Value);
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="guidRequest"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("{Guid}")]
        [ProducesResponseType(typeof(EmployeeTable), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PatchAsync([FromRoute] GuidRequest guidRequest, [FromBody] PutEmployeeRequest request)
        {
            var employeeManager = new EmployeeManager(_context);
            var result = await employeeManager.UpdateAsync(guidRequest.Guid, request);
            return Ok(result);
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
            var result = await _context.Employees.FirstOrDefaultAsync(v => v.Guid.Equals(request.Guid));
            _context.Employees.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
