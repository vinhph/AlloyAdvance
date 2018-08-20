using AlloyAdvanced.Models.Pages;
using AlloyAdvanced.Models.ViewModels;
using EPiServer.Notification;
using EPiServer.Shell.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AlloyAdvanced.Controllers
{
    public class NotificationsPageController : PageControllerBase<NotificationsPage>
    {
        // some services that we need
        private readonly INotifier notifier;
        private readonly IUserNotificationRepository userNotificationRepository;
        private readonly QueryableNotificationUserService queryableNotificationUserService;
        private readonly UIUserProvider userProvider;

        public NotificationsPageController(INotifier notifier,
            IUserNotificationRepository userNotificationRepository,
            QueryableNotificationUserService queryableNotificationUserService,
            UIUserProvider userProvider)
        {
            this.notifier = notifier;
            this.userNotificationRepository = userNotificationRepository;
            this.queryableNotificationUserService = queryableNotificationUserService;
            this.userProvider = userProvider;
        }

        // Notifications API is asynchronous
        // channel parameter can be used to filter messages
        public async Task<ActionResult> Index(NotificationsPage currentPage, string channel)
        {
            // always reset the Ignore property to an empty dictionary
            currentPage.Notifications = new Dictionary<string, PagedUserNotificationMessageResult>();

            // get a list of the first 30 registered users
            IEnumerable<IUIUser> users = userProvider.GetAllUsers(
                pageIndex: 0, pageSize: 30, totalRecords: out int totalRecords);

            foreach (IUIUser user in users)
            {
                // get an object that represents notifications for each user
                INotificationUser notificationUser = await queryableNotificationUserService.GetAsync(user.Username);

                // build a query that includes read and unread and sent and unsent messages
                var query = new UserNotificationsQuery
                {
                    Read = null, // include read and unread
                    Sent = null, // include sent and unsent
                    User = notificationUser
                };

                // if a channel name is set, use it to filter the notifications
                if (!string.IsNullOrWhiteSpace(channel))
                {
                    ViewData["channel"] = channel;
                    query.ChannelName = channel;
                }

                // execute the query
                var result = await userNotificationRepository.GetUserNotificationsAsync(
                        query, startIndex: 0, maxRows: 20);

                // store the query results for the user
                currentPage.Notifications.Add(user.Username, result);
            }

            return View(PageViewModel.Create(currentPage));
        }
    }
}