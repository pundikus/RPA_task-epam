namespace RPA_Onliner_Bot.Service.Abstract
{
    public interface IEmailService
    {
        bool SendMessage(string path, string address);
    }
}
