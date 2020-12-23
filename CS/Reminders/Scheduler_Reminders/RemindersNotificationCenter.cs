using Xamarin.Forms;
using System.Collections.Generic;
using DevExpress.XamarinForms.Scheduler;

namespace Scheduler_Reminders {
    public interface INotificationCenter {
        void UpdateNotifications(IList<TriggeredReminder> reminders, int maxCount);
    }

    public class RemindersNotificationCenter {
        const int MaxNotificationsCount = 32;

        readonly INotificationCenter notificationCenter;

        public RemindersNotificationCenter() {
            this.notificationCenter = DependencyService.Get<INotificationCenter>();
        }

        public void UpdateNotifications(SchedulerDataStorage storage) {
            IList<TriggeredReminder> futureReminders = 
                                        storage.GetNextReminders(MaxNotificationsCount);
            notificationCenter.UpdateNotifications(futureReminders, MaxNotificationsCount);
        }
    }
}
