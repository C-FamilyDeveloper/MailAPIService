namespace MailAPIService.Models.Configs
{
    public class Config
    {
        public MailServerInfo MailServerInfo { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class MailServerInfo
    {
        public string ServerAddress { get; set; }
        public string AuthPassword { get; set; }
        public string SMTPHost { get; set; }
        public string SMTPPort { get; set; }
        public string DisplayName { get; set; }
    }

    public class ConnectionStrings
    {
        public string Connection { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

}
