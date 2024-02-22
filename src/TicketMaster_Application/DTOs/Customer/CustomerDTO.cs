using TicketMaster_Application.DTOs.Abstract.Base;

namespace TicketMaster_Application.DTOs.Customer;
public class CustomerDTO : BaseDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Password { get; set; }
}

