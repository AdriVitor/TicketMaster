using Microsoft.AspNetCore.Mvc;
using TicketMaster_Application.DTOs.Event;
using TicketMaster_Application.DTOs.Ticket;
using TicketMaster_Application.Interfaces;

namespace TicketMaster_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketReadDTO>> GetById([FromRoute] int id) {
            try {
                var ticket = await _ticketService.GetById(id);
                return Ok(ticket);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicketCreateDTO ticket) {
            try {
                await _ticketService.Add(ticket);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Put([FromBody] TicketCreateDTO ticket) {
            try {
                await _ticketService.Update(ticket);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id) {
            try {
                await _ticketService.Delete(id);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("actives/{CustomerId}")]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetTicketsActives([FromRoute] int CustomerId) {
            try {
                var tickets = await _ticketService.GetTicketsActives(CustomerId);
                return Ok(tickets);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/not/actives/{CustomerId}")]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetTicketsNotActives([FromRoute] int CustomerId) {
            try {
                var tickets = await _ticketService.GetTicketsNotActives(CustomerId);
                return Ok(tickets);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}
