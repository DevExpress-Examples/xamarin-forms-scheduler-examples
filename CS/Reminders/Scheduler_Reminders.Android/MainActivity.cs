using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Xamarin.Essentials;

namespace Scheduler_Reminders.Droid {
    [Activity(Label = "Scheduler_Reminders", Icon = "@mipmap/icon", 
              Theme = "@style/MainTheme", MainLauncher = true, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | 
                                     ConfigChanges.UiMode | ConfigChanges.ScreenLayout | 
                                     ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, 
                                                        [GeneratedEnum] Permission[] grantResults) {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent) {
            base.OnNewIntent(intent);
            App application = Xamarin.Forms.Application.Current as App;
            if (application == null) {
                LoadApplication(new App());
                return;
            }
            Guid reminderId = intent.GetReminderId();
            if (reminderId != Guid.Empty)
                application.ProcessNotification(reminderId, intent.GetRecurrenceIndex());
        }
    }
}