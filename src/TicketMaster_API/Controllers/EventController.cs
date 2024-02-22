using Microsoft.AspNetCore.Mvc;
using TicketMaster_Application.DTOs.Event;
using TicketMaster_Application.Interfaces;
using TicketMaster_Domain.Entities.Enums;
using TicketMaster_Domain.Entities;

namespace TicketMaster_API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventReadDTO>> GetById([FromRoute] int id) {
            try {
                var @event = await _eventService.GetById(id);
                return Ok(@event);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EventCreateDTO @event) {
            try {
                await _eventService.Add(@event);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Put([FromBody] EventCreateDTO @event) {
            try {
                await _eventService.Update(@event);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id) {
            try {
                await _eventService.Delete(id);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("uf")]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetByFederativeUnit([FromQuery] EnumFederativeUnit federativeUnit) {
            try {
                var events = await _eventService.GetByFederativeUnit(federativeUnit);
                return Ok(events);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
