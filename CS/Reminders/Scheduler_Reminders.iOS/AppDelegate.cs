using System;
using Foundation;
using UIKit;
using UserNotifications;

namespace Scheduler_Reminders.iOS {
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            global::Xamarin.Forms.Forms.Init();
            DevExpress.XamarinForms.Scheduler.iOS.Initializer.Init();
            App formsApplication = new App();
            UNUserNotificationCenter.Current.Delegate =
                                    new CustomUserNotificationCenterDelegate(formsApplication);
            LoadApplication(formsApplication);

            return base.FinishedLaunching(app, options);
        }
    }

    public class CustomUserNotificationCenterDelegate : UNUserNotificationCenterDelegate {
        readonly App app;

        public CustomUserNotificationCenterDelegate(App app) {
            this.app = app;
        }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center,
                                                            UNNotificationResponse response,
                                                            Action completionHandler) {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            string identifier = response.Notification.Request.Identifier;
            string recurrenceId = identifier.Split(':')[0];
            int recurrenceIndex = Int32.Parse(identifier.Split(':')[1]);
            Guid reminderGuid = Guid.Parse(recurrenceId);
            app.ProcessNotification(reminderGuid, recurrenceIndex);
            completionHandler();
        }
        public override void WillPresentNotification(
            UNUserNotificationCenter center, UNNotification notification,
            Action<UNNotificationPresentationOptions> completionHandler) {
            completionHandler(UNNotificationPresentationOptions.Alert |
                              UNNotificationPresentationOptions.Badge |
                              UNNotificationPresentationOptions.Badge);
        }
    }
}
