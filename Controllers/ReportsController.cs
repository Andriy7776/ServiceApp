using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceApp.Models;

namespace ServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public ReportsController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problem>>> GetReports()
        {
            return await _context.Problems.ToListAsync();
        }

        // POST: api/reports
        [HttpPost]
        public async Task<ActionResult<Problem>> PostReport(Problem report)
        {
            _context.Problems.Add(report);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReports), new { id = report.Id }, report);
        }

        // PUT: api/reports/{id}/assign/{employeeId}
        [HttpPut("{id}/assign/{employeeId}")]
        public async Task<IActionResult> AssignEmployee(int id, int employeeId)
        {
            var report = await _context.Problems.FindAsync(id);
            if (report == null)
                return NotFound();

            report.EmployeeId = employeeId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/reports/{id}/close
        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseReport(int id)
        {
            var report = await _context.Problems.FindAsync(id);
            if (report == null)
                return NotFound();

            report.CloseDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}