using TicketMaster_Domain.Abstract;
using TicketMaster_Domain.Validations;

namespace TicketMaster_Domain.Entities;
public class Customer : BaseUsers {
    public List<Ticket> Tickets { get; private set; }

    public Customer()
    {
        
    }

    public Customer(string name, string email, string cpf, string password, List<Ticket>? tickets = null)
    {
        DomainExceptionValidation.When(name == null, "O campo nome é obrigatório");
        DomainExceptionValidation.When(email == null || !email.Contains("@"), "Digite um email válido");
        DomainExceptionValidation.When(cpf == null || cpf.Length != 11, "Digite um CPF válido");
        DomainExceptionValidation.When(password == null || password.Length < 8, "Digite uma senha válida");

        Name = name;
        Email = email;
        CPF = cpf;
        Password = password;
        Tickets = tickets;
    }
}

