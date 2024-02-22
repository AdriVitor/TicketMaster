using TicketMaster_Domain.Abstract;
using TicketMaster_Domain.Validations;

namespace TicketMaster_Domain.Entities;
public class Producer : BaseUsers {
    public List<Event> Events { get; private set; }

    public Producer()
    {
        
    }

    public Producer(string name, string email, string cpf, string password, List<Event>? events = null)
    {
        DomainExceptionValidation.When(name == null, "O campo nome é obrigatório");
        DomainExceptionValidation.When(email == null || !email.Contains("@"), "Digite um email válido");
        DomainExceptionValidation.When(cpf == null || cpf.Length != 11, "Digite um CPF válido");
        DomainExceptionValidation.When(password == null || password.Length < 8, "Digite uma senha válida");

        Name = name;
        Email = email;
        CPF = cpf;
        Password = password;
        Events = events;
    }
}

