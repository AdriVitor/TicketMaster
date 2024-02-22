using Microsoft.AspNetCore.Mvc;
using TicketMaster_Application.DTOs.Customer;
using TicketMaster_Application.Interfaces;
using TicketMaster_Domain.Entities;

namespace TicketMaster_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase {
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService) {
        _customerService = customerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById([FromRoute] int id) {
        try {
            var customer = await _customerService.GetById(id);
            return Ok(customer);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CustomerDTO customer) {
        try {
            await _customerService.Add(customer);
            return Ok();
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> Put([FromBody] CustomerDTO customer) {
        try {
            await _customerService.Update(customer);
            return Ok();
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id) {
        try {
            await _customerService.Delete(id);
            return Ok();
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}
