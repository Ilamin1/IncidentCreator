using IncidentCreator.Application.Dtos;
using IncidentCreator.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IncidentCreator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentsController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] IncidentCreationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var incidentName = await _incidentService.CreateIncidentProcessAsync(request);

                if (incidentName == null)
                {
                    return NotFound($"Account with name '{request.AccountName}' not found.");
                }
                
                return CreatedAtAction(nameof(Create), new { incidentName }, new { incidentName });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred. {ex.Message}");
            }
        }
    }
}