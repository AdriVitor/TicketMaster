namespace TicketMaster_Domain.Entities;

public class ConfigurationEmail {
    private static ConfigurationEmail instance;
    private static readonly object padLock = new object();

    public string SmtpServer { get; private set; }
    public int SmtpPort { get; private set; }
    public string SmtpUsername { get; private set; }
    public string SmtpPassword { get; private set; }

    private ConfigurationEmail() {
        SmtpServer = "smtp.gmail.com";
        SmtpPort = 587;
        SmtpUsername = "TicketMaster101010@gmail.com";
        SmtpPassword = "fepq mpon gobl iqrf";
    }

    public static ConfigurationEmail Instance {
        get {
            lock(padLock) {
                if (instance == null) {
                    instance = new ConfigurationEmail();
                }
                return instance;
            }           
        }
    }
}

