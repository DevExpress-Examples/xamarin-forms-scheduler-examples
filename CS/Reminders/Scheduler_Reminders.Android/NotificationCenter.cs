using Android.App;
using Android.Content;
using AndroidX.Core.App;
using Java.Sql;
using Scheduler_Reminders.Droid;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AAplication = Android.App.Application;
using DevExpress.XamarinForms.Scheduler;

[assembly: Dependency(typeof(NotificationCenter))]

namespace Scheduler_Reminders.Droid {
    public class NotificationCenter : INotificationCenter {
        static Date ToNativeDate(DateTime dateTime) {
            long dateTimeUtcAsMilliseconds =
                (long)dateTime.ToUniversalTime().
                Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            return new Date(dateTimeUtcAsMilliseconds);
        }

        void INotificationCenter.UpdateNotifications(IList<TriggeredReminder> reminders, 
                                                    int maxCount) {
            AlarmManager alarm = 
                (AlarmManager)AAplication.Context.GetSystemService(Context.AlarmService);
            for (int i = 0; i < maxCount; i++) {
                if (i < reminders.Count) {
                    TriggeredReminder reminder = reminders[i];
                    var pendingIntent = PendingIntent.GetBroadcast(AAplication.Context, i, 
                                                    CreateIntent(reminder), 
                                                    PendingIntentFlags.UpdateCurrent);
                    alarm.Cancel(pendingIntent);
                    AlarmManagerCompat.SetExactAndAllowWhileIdle(alarm, 
                                                    (int)AlarmType.RtcWakeup,
                                                    ToNativeDate(reminder.AlertTime).Time, 
                                                    pendingIntent);
                }
                else {
                    var pendingIntent = PendingIntent.GetBroadcast(AAplication.Context, i,
                                                    CreateIntent(), 
                                                    PendingIntentFlags.UpdateCurrent);
                    alarm.Cancel(pendingIntent);
                }
            }
        }

        Intent CreateIntent() {
            return new Intent(AAplication.Context, typeof(NotificationAlarmHandler)).
                        SetAction(NotificationAlarmHandler.NotificationHandler);
        }
        Intent CreateIntent(string id, int recurrenceIndex, string subject, string interval) {
            return CreateIntent()
                .PutExtra(NotificationAlarmHandler.ReminderId, id)
                .PutExtra(NotificationAlarmHandler.RecurrenceIndex, recurrenceIndex)
                .PutExtra(NotificationAlarmHandler.Subject, subject)
                .PutExtra(NotificationAlarmHandler.Interval, interval);
        }
        Intent CreateIntent(TriggeredReminder reminder) {
            AppointmentItem appointment = reminder.Appointment;
            return CreateIntent(reminder.Id.ToString(), 
                                appointment.RecurrenceIndex, 
                                appointment.Subject, 
                                appointment.Interval.ToString("{0:g} - {1:g}", null));
        }
    }
}