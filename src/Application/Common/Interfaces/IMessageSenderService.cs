namespace Intaker.Application.Common.Interfaces;
public interface IMessageSenderService
{
    Task SendMessage(string message);
}
