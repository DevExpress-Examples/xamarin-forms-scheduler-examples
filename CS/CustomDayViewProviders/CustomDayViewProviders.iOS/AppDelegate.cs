using DevExpress.XamarinForms.Scheduler;
using DevExpress.XamarinForms.Scheduler.iOS;
using Foundation;
using UIKit;

namespace CustomDayViewProviders.iOS
{
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

            DayViewProviderService.RegisterViewProviderService(new CustomDayViewProviderService());

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }

    public class CustomDayViewProviderService : DayViewProviderService {
        public override IViewProvider CreateCellViewProvider(DayView dayView) {
            return new CustomCellViewProvider();
        }

        public override IViewProvider CreateDateHeaderViewProvider(DayView dayView) {
            return new CustomDateHeaderViewProvider();
        }
    }
}
