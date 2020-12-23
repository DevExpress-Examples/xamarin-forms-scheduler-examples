using System;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

namespace Scheduler_Reminders.Droid {
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { NotificationHandler })]
    public class NotificationAlarmHandler : BroadcastReceiver {
        public const string NotificationHandler = "NotificationAlarmHandler";
        public const string ReminderId = "ReminderId";
        public const string RecurrenceIndex = "RecurrenceIndex";
        public const string Subject = "Subject";
        public const string Interval = "Interval";

        static Intent GetLauncherActivity() {
            var packageName = Application.Context.PackageName;
            return Application.Context.PackageManager.GetLaunchIntentForPackage(packageName);
        }

        string CurrentPackageName => Application.Context.PackageName;
        string ReminderChannelId => $"{CurrentPackageName}.reminders";
        NotificationManager NotificationManager => (NotificationManager)Application.Context.
                                                    GetSystemService(Context.NotificationService);

        public NotificationAlarmHandler() {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                NotificationManager.CreateNotificationChannel(
                    new NotificationChannel(ReminderChannelId, "Reminders", 
                                            NotificationImportance.High));
        }

        public override void OnReceive(Context context, Intent intent) {
            Guid reminderId = intent.GetReminderId();
            if (reminderId == Guid.Empty)
                return;

            int notificationId = reminderId.GetHashCode();

            Intent resultIntent = GetLauncherActivity().PutExtras(intent.Extras).
                                  SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);
            PendingIntent resultPendingIntent = PendingIntent.GetActivity(
                                                                context,
                                                                notificationId,
                                                                resultIntent,
                                                                PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = 
                                        new NotificationCompat.Builder(Application.Context);
            builder.SetContentIntent(resultPendingIntent);
            builder.SetDefaults((int)NotificationDefaults.All);
            builder.SetContentTitle(intent.GetStringExtra(Subject));
            builder.SetContentText(intent.GetStringExtra(Interval));
            builder.SetSmallIcon(Resource.Mipmap.app_icon);
            builder.SetChannelId(ReminderChannelId);
            builder.SetPriority((int)NotificationPriority.High);
            builder.SetAutoCancel(true);
            builder.SetVisibility((int)NotificationVisibility.Public);
            NotificationManager.Notify(notificationId, builder.Build());
        }
    }

    public static class IntentExtensions {
        public static Guid GetReminderId(this Intent intent) {
            string guidString = intent.GetStringExtra(NotificationAlarmHandler.ReminderId);
            try {
                return Guid.Parse(guidString);
            }
            catch {
                return Guid.Empty;
            }
        }

        public static int GetRecurrenceIndex(this Intent intent) {
            return intent.GetIntExtra(NotificationAlarmHandler.RecurrenceIndex, -1);
        }
    }
}