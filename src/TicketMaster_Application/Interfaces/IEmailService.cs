using System.Net.Mail;

namespace TicketMaster_Application.Interfaces;
public interface IEmailService {
    public void SendEmail(string recipient, string subject, string body);
    public void SendEmailNewUser(string userName, string userEmail);
    public void SendEmailNewTicket(string customerName, string customerEmail, string eventName);
    public void SendEmailNewEvent(string nameProducer, string emailProducer);
}

