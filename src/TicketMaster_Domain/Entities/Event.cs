using TicketMaster_Domain.Abstract;
using TicketMaster_Domain.Entities.Enums;
using TicketMaster_Domain.Validations;

namespace TicketMaster_Domain.Entities;
    public class Event : Base {
    public string Name { get; private set; }
    public string Description { get; private set; }
    public EnumFederativeUnit FederativeUnit { get; private set; }
    public DateTime Date { get; private set; }
    public int ProducerId { get; private set; }
    public Producer Producer { get; private set; }
    public int TotalAmount { get; private set; }
    public int CurrentQuantityTickets { get; private set; }

    public Event()
    {
        
    }

    public static Event Create(int id, int producerId, string name, string description, EnumFederativeUnit federativeUnit, DateTime date, int totalAmount)
    {
        DomainExceptionValidation.When(id < 0, "Digite um id válido");
        DomainExceptionValidation.When(producerId < 0, "Selecione um produtor de eventos válido");
        DomainExceptionValidation.When(name == null, "O campo nome é obrigatório");
        DomainExceptionValidation.When(description == null, "O campo descrição é obrigatório");
        DomainExceptionValidation.When((int)federativeUnit < 0 || (int)federativeUnit > 26, "Escolha uma unidade federativa válida");
        DomainExceptionValidation.When(date < DateTime.Now, "Digite uma data válida");
        DomainExceptionValidation.When(totalAmount < 0, "Digite uma quantidade válida de pessoas suportadas no evento");

        return new Event{
            Id = id,
            ProducerId = producerId,
            Name = name,
            Description = description,
            FederativeUnit = federativeUnit,
            Date = date,
            TotalAmount = totalAmount
        };
        
    }

    public void CurrentQuantityTicketInitial(int totalAmount) {
        CurrentQuantityTickets = totalAmount;
    }
}

