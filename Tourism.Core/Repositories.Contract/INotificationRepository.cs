using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Repositories.Contract
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationDTO>> GetAllNotificationAsync();

    }
}
