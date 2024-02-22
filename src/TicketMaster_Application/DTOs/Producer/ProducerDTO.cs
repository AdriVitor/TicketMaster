using TicketMaster_Application.DTOs.Abstract.Base;

namespace TicketMaster_Application.DTOs.Producer;
public class ProducerDTO {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Password { get; set; }
}

