using System.Net;
using System.Net.Mail;
using TicketMaster_Application.Interfaces;
using TicketMaster_Domain.Entities;
using TicketMaster_Domain.Entities.Enums;

namespace TicketMaster_Application.Services;
public class EmailService : IEmailService {

    public async void SendEmail(string recipient, string subject, string body) {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(ConfigurationEmail.Instance.SmtpUsername);
            emailMessage.To.Add(recipient);
            emailMessage.Subject = subject;
            emailMessage.Body = body;

            var smtpClient = SMTPCLientDefaultConfiguration();
            await smtpClient.SendMailAsync(emailMessage);
            smtpClient.Dispose();            
    }

    private SmtpClient SMTPCLientDefaultConfiguration() {
        var smtpClient = new SmtpClient(ConfigurationEmail.Instance.SmtpServer, ConfigurationEmail.Instance.SmtpPort);
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Credentials = new NetworkCredential(ConfigurationEmail.Instance.SmtpUsername, ConfigurationEmail.Instance.SmtpPassword);       

        return smtpClient;
    }

    public void SendEmailNewUser(string userName, string userEmail) {
        var subjectAndBody = FillBodyAndSubject(EnumTypeEmail.newUser, userName);

        SendEmail(userEmail, subjectAndBody.Key, subjectAndBody.Value);
    }

    public void SendEmailNewTicket(string customerName, string customerEmail, string eventName) {
        var subjectAndBody = FillBodyAndSubject(EnumTypeEmail.newTicketCustomer, customerName, eventName);

        SendEmail(customerEmail, subjectAndBody.Key, subjectAndBody.Value);
    }

    public void SendEmailNewEvent(string eventName, string emailProducer) {
        var subjectAndBody = FillBodyAndSubject(EnumTypeEmail.newEvent, null, eventName);

        SendEmail(emailProducer, subjectAndBody.Key, subjectAndBody.Value);
    }

    private KeyValuePair<string, string> FillBodyAndSubject(EnumTypeEmail subjectAndBodyEmail, string? userName = null, string? eventName = null) {
        var subjectAndBody = new Dictionary<string, string>() { };
        switch (subjectAndBodyEmail) {
            case EnumTypeEmail.newEvent:
                subjectAndBody.Add("Novo Evento Ticket Master", $"{eventName} cadastrado com sucesso.");
                return subjectAndBody.FirstOrDefault();
            case EnumTypeEmail.newUser:
                subjectAndBody.Add("Ticket Master", $"Olá {userName}, Seja bem-vindo ao Ticket Master.");
                return subjectAndBody.FirstOrDefault();
            case EnumTypeEmail.newTicketCustomer:
                subjectAndBody.Add("Novo Ingresso - Ticket Master", $"Olá {userName}, Parabéns para a compra do(s) ingresso(s) para o evento {eventName}");
                return subjectAndBody.FirstOrDefault();
            default:
                return subjectAndBody.FirstOrDefault();
        }
    }

    public Dictionary<string, string> DefaultSujectAndEmailNewUser(string userName) {
        var subjectAndBody = new Dictionary<string, string>() {
                                    { "Ticket Master", $"Olá {userName}, Seja bem-vindo ao Ticket Master." }
                                 };

        return subjectAndBody;
    }

    public Dictionary<string, string> DefaultSujectAndEmailNewEvent(string eventName) {
        var subjectAndBody = new Dictionary<string, string>() {
                                    { "Novo Evento Ticket Master", $"{eventName} cadastrado com sucesso." }
                                 };

        return subjectAndBody;
    }

    public Dictionary<string, string> DefaultSujectAndEmailNewTicketCustomer(string userName, string eventName) {
        var subjectAndBody = new Dictionary<string, string>() {
                                    { "Novo Ingresso - Ticket Master", $"Olá {userName}, Parabéns para a compra do(s) ingresso(s) para o evento {eventName}" }
                                 };

        return subjectAndBody;
    }
}

