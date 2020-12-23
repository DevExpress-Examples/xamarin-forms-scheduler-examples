using System;
using Xamarin.Forms;

namespace Scheduler_Reminders {
    public partial class App : Application {
        readonly MainPage page;
        public App() {
            InitializeComponent();

            // MainPage = new MainPage();
            page = new MainPage();
            MainPage = new NavigationPage(page);
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }

        public void ProcessNotification(Guid reminderId, int recurrenceIndex) {
            if (reminderId == Guid.Empty)
                return;
            page.ShowAppointmentEditPage(reminderId, recurrenceIndex);
        }
    }
}
