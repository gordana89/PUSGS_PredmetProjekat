namespace UsersMicroservice.Domain.Abstractions
{
    public interface IMailService
    {
        void SendMail(string delivererMail, string firstName, string lastName);
    }
}
