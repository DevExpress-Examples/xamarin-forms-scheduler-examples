using Xamarin.Forms;

namespace Scheduler_GettingStarted {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            // MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
