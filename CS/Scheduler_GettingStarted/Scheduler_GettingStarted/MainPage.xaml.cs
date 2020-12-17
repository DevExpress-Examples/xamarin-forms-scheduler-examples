using Xamarin.Forms;
using DevExpress.XamarinForms.Scheduler;

namespace Scheduler_GettingStarted {
    public partial class MainPage : ContentPage {
        public MainPage() {
            DevExpress.XamarinForms.Scheduler.Initializer.Init();
            InitializeComponent();
        }

        private void WorkWeekView_Tap(object sender, DevExpress.XamarinForms.Scheduler.SchedulerGestureEventArgs e) {
            if (e.AppointmentInfo == null) {
                ShowNewAppointmentEditPage(e.IntervalInfo);
                return;
            }
            AppointmentItem appointment = e.AppointmentInfo.Appointment;
            ShowAppointmentEditPage(appointment);
        }

        private void ShowAppointmentEditPage(AppointmentItem appointment) {
            AppointmentEditPage appEditPage = new AppointmentEditPage(appointment, this.storage);
            Navigation.PushAsync(appEditPage);
        }

        private void ShowNewAppointmentEditPage(IntervalInfo info) {
            AppointmentEditPage appEditPage = new AppointmentEditPage(info.Start, info.End,
                                                                     info.AllDay, this.storage);
            Navigation.PushAsync(appEditPage);
        }
    }
}
