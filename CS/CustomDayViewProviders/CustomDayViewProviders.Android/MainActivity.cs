
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using DevExpress.XamarinForms.Scheduler.Android;
using DevExpress.XamarinForms.Scheduler;
using System.Collections;
using Android.Views;
using DevExpress.XamarinAndroid.Scheduler.Visual.Data;
using Android.Content;

namespace CustomDayViewProviders.Droid {
    [Activity(Label = "CustomDayViewProviders", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            DayViewProviderService.RegisterViewProviderService(new CustomDayViewProviderService());

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }


    public class CustomDayViewProviderService : DayViewProviderService {
        public override IViewProvider CreateCellViewProvider(DayView dayView) {
            return new CustomDayCellViewProvider();
        }

        public override IViewProvider CreateDateHeaderViewProvider(DayView dayView) {
            return new CustomDateHeaderViewProvider();
        }
    }
}