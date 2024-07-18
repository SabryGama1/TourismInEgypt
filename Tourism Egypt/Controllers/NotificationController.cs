using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationRepository _notificationService;
        public NotificationController(INotificationRepository notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotification()
        {

            var notificationReviews = await _notificationService.GetAllNotificationAsync();

            if (notificationReviews == null)
            {
                return NotFound("No Existing Notification");
            }
            else
                return Ok(notificationReviews);
        }

    }
}