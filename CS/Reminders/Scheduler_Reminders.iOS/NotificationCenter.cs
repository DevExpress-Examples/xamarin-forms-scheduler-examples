using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UserNotifications;
using Xamarin.Forms;
using DevExpress.XamarinForms.Scheduler;

[assembly: Dependency(typeof(Scheduler_Reminders.iOS.NotificationCenter))]

namespace Scheduler_Reminders.iOS {
    public class NotificationCenter : INotificationCenter {
        public class ReminderIdentifier {
            public Guid Guid { get; private set; }
            public int RecurrenceIndex { get; private set; }

            public ReminderIdentifier(Guid guid, int recurrenceIndex) {
                Guid = guid;
                RecurrenceIndex = recurrenceIndex;
            }
        }
        public static string SerializeReminder(TriggeredReminder reminder) {
            return reminder.Id + ":" + reminder.Appointment.RecurrenceIndex.ToString();
        }
        public static ReminderIdentifier DeserializeReminder(string reminderIdentifier) {
            string[] splitData = reminderIdentifier.Split(':');
            string recurrenceId = splitData[0];
            int recurrenceIndex = Int32.Parse(splitData[1]);
            Guid reminderGuid = Guid.Parse(recurrenceId);
            return new ReminderIdentifier(reminderGuid, recurrenceIndex);
        }

        readonly ReminderNotificationCenterVersion notificationsCore;

        public NotificationCenter() {
            notificationsCore = new ReminderNotificationCenterVersion();
        }

        public void UpdateNotifications(IList<TriggeredReminder> reminders, int maxCount) {
            notificationsCore.UpdateRemindersNotifications(reminders);
        }
    }


    // Send notifications via the UNUserNotificationCenter system.
    public class ReminderNotificationCenterVersion {
        readonly UNUserNotificationCenter notificationCenter =
                                                UNUserNotificationCenter.Current;

        string CreateMessageContent(TriggeredReminder reminder) {
            return reminder.Appointment.Interval.ToString("{0:g} - {1:g}", null);
        }

        Task<Tuple<bool, NSError>> RequestUserAccess() {
            UNAuthorizationOptions options = UNAuthorizationOptions.Alert |
                                             UNAuthorizationOptions.Sound |
                                             UNAuthorizationOptions.Badge;
            return notificationCenter.RequestAuthorizationAsync(options);
        }
        async void ScheduleReminderNotification(TriggeredReminder reminder, int badge) {
            UNMutableNotificationContent content = new UNMutableNotificationContent() {
                Title = reminder.Appointment.Subject,
                Body = CreateMessageContent(reminder),
                Sound = UNNotificationSound.Default,
                Badge = badge,
            };
            NSDateComponents dateComponents = new NSDateComponents() {
                Second = reminder.AlertTime.Second,
                Minute = reminder.AlertTime.Minute,
                Hour = reminder.AlertTime.Hour,
                Day = reminder.AlertTime.Day,
                Month = reminder.AlertTime.Month,
                Year = reminder.AlertTime.Year,
                TimeZone = NSTimeZone.SystemTimeZone,
            };
            UNCalendarNotificationTrigger trigger =
                UNCalendarNotificationTrigger.CreateTrigger(dateComponents, false);
            string identifier = NotificationCenter.SerializeReminder(reminder);
            UNNotificationRequest request =
                UNNotificationRequest.FromIdentifier(identifier, content, trigger);
            await notificationCenter.AddNotificationRequestAsync(request);
        }

        public async void UpdateRemindersNotifications(IList<TriggeredReminder> featureReminders) {
            Tuple<bool, NSError> authResult = await RequestUserAccess();
            if (!authResult.Item1) {
                Debug.WriteLine("User denied access to notifications");
                return;
            }
            notificationCenter.RemoveAllPendingNotificationRequests();
            for (int i = 0; i < featureReminders.Count; i++) {
                ScheduleReminderNotification(featureReminders[i], i + 1);
            }
        }
    }
}