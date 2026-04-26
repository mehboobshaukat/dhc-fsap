namespace DHC_FSAP.Services.Auth
{
    public interface IEmailService
    {
        void SendEmail(string to, string  subject, string body);
    }
}
