using TicketMaster_Domain.Abstract;
using TicketMaster_Domain.Validations;

namespace TicketMaster_Domain.Entities;
public class Ticket : Base {
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public int EventId { get; private set; }
    public Event Event { get; private set; }
    public bool FlagConsumed { get; private set; }

    public Ticket() {

    }

    public Ticket(int customerId, int eventId, bool flagConsumed) {
        DomainExceptionValidation.When(customerId < 0, "Informe um cliente válido");
        DomainExceptionValidation.When(eventId < 0, "Informe um evento válido");
        
        CustomerId = customerId;
        EventId = eventId;
        FlagConsumed = flagConsumed;
    }
}

