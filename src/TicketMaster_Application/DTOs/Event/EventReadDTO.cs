using TicketMaster_Application.DTOs.Abstract.Base;
using TicketMaster_Application.DTOs.Producer;
using TicketMaster_Domain.Entities.Enums;

namespace TicketMaster_Application.DTOs.Event;
public class EventReadDTO : BaseDTO {
    public string Name { get; set; }
    public string Description { get; set; }
    public EnumFederativeUnit FederativeUnit { get; set; }
    public DateTime Date { get; set; }
    public int TotalAmount { get; set; }
    public int CurrentQuantityTickets { get; set; }
}

