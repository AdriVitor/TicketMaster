using TicketMaster_Application.DTOs.Abstract.Base;
using TicketMaster_Application.DTOs.Event;

namespace TicketMaster_Application.DTOs.Ticket;
public class TicketReadListDTO : BaseDTO {
    public int CustomerId { get; set; }
    public EventReadDTO Event { get; set; }
    public bool FlagConsumed { get; set; }
}

