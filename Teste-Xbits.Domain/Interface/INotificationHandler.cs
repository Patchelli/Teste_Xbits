using Teste_Xbits.Domain.Handlers.NotificationHandler;

namespace Teste_Xbits.Domain.Interface;

public interface INotificationHandler
{
    List<DomainNotification> GetNotifications();
    bool HasNotification();
    void CreateNotifications(IEnumerable<DomainNotification> notifications);
    void CreateNotification(DomainNotification notification);
    bool CreateNotification(string key, string value);
    void DeleteNotification(DomainNotification notification);
}