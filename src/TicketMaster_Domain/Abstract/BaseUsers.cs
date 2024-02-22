namespace TicketMaster_Domain.Abstract;
public abstract class BaseUsers : Base {
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string CPF { get; protected set; }
    public string Password { get; protected set; }
}

