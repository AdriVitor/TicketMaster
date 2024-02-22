using System.Net.Mail;

namespace TicketMaster_Application.Interfaces;
public interface IEmailService {
    public void SendEmail(string recipient, string subject, string body);
    public Dictionary<string, string> DefaultSujectAndEmailNewUser(string userName);
    public Dictionary<string, string> DefaultSujectAndEmailNewEvent(string eventName);
    public Dictionary<string, string> DefaultSujectAndEmailNewTicketCustomer(string userName, string eventName);
    public void SendEmailNewUser(string userName, string userEmail);
    public void SendEmailNewTicket(string customerName, string customerEmail, string eventName);
    public void SendEmailNewEvent(string nameProducer, string emailProducer);
}

