using Microsoft.AspNetCore.Mvc;
using TicketMaster_Application.DTOs.Producer;
using TicketMaster_Application.Interfaces;
using TicketMaster_Domain.Entities;

namespace TicketMaster_API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProducerController : ControllerBase {
    private readonly IProducerService _producerService;
    public ProducerController(IProducerService producerService) {
        _producerService = producerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Producer>> GetById([FromRoute] int id) {
        try {
            var producer = await _producerService.GetById(id);
            return Ok(producer);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProducerDTO producer) {
        try {
            await _producerService.Add(producer);
            return Ok();
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> Put([FromBody] ProducerDTO producer) {
        try {
            await _producerService.Update(producer);
            return Ok();
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id) {
        try {
            await _producerService.Delete(id);
            return Ok();
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}


