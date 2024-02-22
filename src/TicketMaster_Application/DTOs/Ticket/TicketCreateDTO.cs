using TicketMaster_Application.DTOs.Abstract.Base;

namespace TicketMaster_Application.DTOs.Ticket;
public class TicketCreateDTO : BaseDTO {
    public int CustomerId { get; set; }
    public int EventId { get; set; }
    public bool FlagConsumed { get; set; }
}

