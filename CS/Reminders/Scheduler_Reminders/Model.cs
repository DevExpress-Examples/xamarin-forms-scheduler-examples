using System;
using System.Collections.ObjectModel;

namespace Scheduler_Reminders {
    public class ReminderAppointment {
        public int Id { get; set; }
        public bool AllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Subject { get; set; }
        public int Label { get; set; }
        public int Status { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
    }

    class AppointmentData {
        static Random rnd = new Random();

        void CreateAppointments() {
            int appointmentId = 1;
            DateTime start;
            TimeSpan duration;

            ObservableCollection<ReminderAppointment> result = 
                                        new ObservableCollection<ReminderAppointment>();

            for (int i = 0; i < 5; i++) {
                ReminderAppointment appointmentWithReminder = new ReminderAppointment();
                start = DateTime.Now.AddMinutes(1);
                duration = TimeSpan.FromMinutes(rnd.Next(30, 50));
                appointmentWithReminder.Id = appointmentId;
                appointmentWithReminder.Start = start;
                appointmentWithReminder.End = start.Add(duration);
                appointmentWithReminder.Subject = "Test Appointment";
                appointmentWithReminder.Label = rnd.Next(1, 10);
                appointmentWithReminder.Status = rnd.Next(0, 4);
                result.Add(appointmentWithReminder);
                appointmentId++;
            }
            AppointmentRepository = result;
        }

        public ObservableCollection<ReminderAppointment> AppointmentRepository { get; private set; }

        public AppointmentData()
        {
            CreateAppointments();
        }
    }
}
