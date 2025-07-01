using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceApp.Models;
using AutoMapper;

namespace ServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ServiceDbContext _context;
        private readonly IMapper _mapper;

        public EmployeesController(ServiceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(_mapper.Map<List<EmployeeDto>>(employees));
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee(EmployeeDto dto)
        {
            if (dto == null) return BadRequest();

            var entity = _mapper.Map<Employee>(dto);
            _context.Employees.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployees), new { id = entity.Id }, dto);
        }
    }
}