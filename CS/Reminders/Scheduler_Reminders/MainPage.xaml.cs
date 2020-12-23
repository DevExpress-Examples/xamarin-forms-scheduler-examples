using System;
using Xamarin.Forms;
using DevExpress.XamarinForms.Scheduler;

namespace Scheduler_Reminders {
    public partial class MainPage : ContentPage {
        readonly RemindersNotificationCenter remindersNotificationCenter = 
                                                        new RemindersNotificationCenter();
        public MainPage() {
            DevExpress.XamarinForms.Scheduler.Initializer.Init();
            InitializeComponent();
        }

        private void OnTap(object sender, SchedulerGestureEventArgs e) {
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

        public void ShowAppointmentEditPage(Guid reminderId, int recurrenceIndex) {
            AppointmentItem appointment = storage.FindAppointmentByReminder(reminderId);
            if (appointment != null && recurrenceIndex >= 0)
                appointment = storage.GetOccurrenceOrException(appointment, recurrenceIndex);
            if (appointment != null)
                ShowAppointmentEditPage(appointment);
        }

        private void ShowNewAppointmentEditPage(IntervalInfo info) {
            AppointmentEditPage appEditPage = new AppointmentEditPage(info.Start, info.End,
                                                                      info.AllDay, this.storage);
            Navigation.PushAsync(appEditPage);
        }

        void OnRemindersChanged(object sender, EventArgs e) {
            remindersNotificationCenter.UpdateNotifications(storage);
        }


    }

}
