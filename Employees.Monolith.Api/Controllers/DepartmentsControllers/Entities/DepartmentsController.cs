using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.Api.Controllers.DepartmentsControllers.Requests.Entities;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables;
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

namespace Employees.Monolith.Api.Controllers.DepartmentsControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class DepartmentsController : ControllerBase
    {
        private readonly ILogger<DepartmentsController> _logger;
        private readonly DatabaseContext _context;
        public DepartmentsController(ILogger<DepartmentsController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepartmentTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _context.Departments.ToListAsync();
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <returns></returns>
        [HttpGet("avarage-salary")]
        [ProducesResponseType(typeof(IEnumerable<DepartmentResponse>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAvarageSalaryAsync()
        {
            var departments = await _context.Departments
                .Include(v => v.Employees)
                .ToListAsync();
            var result = new List<DepartmentResponse>();
            for (int i = 0; i < departments.Count; i++)
            {
                var avarageSalary = departments[i].Employees.Sum(v => v.Salary);
                if (avarageSalary > 0m && departments[i].Employees.Count > 0)
                {
                    avarageSalary = decimal.Divide(avarageSalary, departments[i].Employees.Count);
                }
                else avarageSalary = 0m;
                var countEmployees = departments[i].Employees.Count;
                departments[i].Employees = null;
                var departmentResponse = new DepartmentResponse(departments[i], avarageSalary, countEmployees);
                result.Add(departmentResponse);
            }
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("avarage-salary/{From}/{To}")]
        [ProducesResponseType(typeof(PaginationResponse<DepartmentResponse>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] PaginationRequest request)
        {
            var take = request.GetTake();
            var totalCount = await _context.Departments.CountAsync();
            var departments = await _context.Departments
                .Include(v => v.Employees)
                .OrderBy(v => v.Id)
                .Skip(request.From)
                .Take(take)
                .ToListAsync();
            var result = new PaginationResponse<DepartmentResponse>(totalCount);
            for (int i = 0; i < departments.Count; i++)
            {
                var avarageSalary = departments[i].Employees.Sum(v => v.Salary);
                if (avarageSalary > 0m && departments[i].Employees.Count > 0)
                {
                    avarageSalary = decimal.Divide(avarageSalary, departments[i].Employees.Count);
                }
                else avarageSalary = 0m;
                var countEmployees = departments[i].Employees.Count;
                departments[i].Employees = null;
                var departmentResponse = new DepartmentResponse(departments[i], avarageSalary, countEmployees);
                result.Values.Add(departmentResponse);
            }
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="paginationRequest"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("avarage-salary/{From}/{To}")]
        [ProducesResponseType(typeof(IEnumerable<DepartmentResponse>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PostAsync([FromRoute] PaginationRequest paginationRequest, [FromBody] PostDepartmentsRequest request)
        {
            IQueryable<DepartmentTable> query = _context.Departments;
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(v => v.Title.Contains(request.Title));
            }
            var take = paginationRequest.GetTake();
            var totalCount = await query.CountAsync();
            var departments = await query
                .Include(v => v.Employees)
                .OrderBy(v => v.Id)
                .Skip(paginationRequest.From)
                .Take(take)
                .ToListAsync();
            var result = new PaginationResponse<DepartmentResponse>(totalCount);
            for (int i = 0; i < departments.Count; i++)
            {
                var avarageSalary = departments[i].Employees.Sum(v => v.Salary);
                if (avarageSalary > 0m && departments[i].Employees.Count > 0)
                {
                    avarageSalary = decimal.Divide(avarageSalary, departments[i].Employees.Count);
                }
                else avarageSalary = 0m;
                var countEmployees = departments[i].Employees.Count;
                departments[i].Employees = null;
                var departmentResponse = new DepartmentResponse(departments[i], avarageSalary, countEmployees);
                result.Values.Add(departmentResponse);
            }
            return Ok(result);
        }
    }
}
