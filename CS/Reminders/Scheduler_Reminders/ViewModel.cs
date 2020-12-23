using System.Collections.Generic;
using System.ComponentModel;

namespace Scheduler_Reminders {
    public class ViewModel : INotifyPropertyChanged {
        readonly AppointmentData data;

        public event PropertyChangedEventHandler PropertyChanged;
        public IReadOnlyList<ReminderAppointment> AppointmentRepository 
                                        { get => data.AppointmentRepository; }

        public ViewModel() {
            data = new AppointmentData();
        }

        protected void RaisePropertyChanged(string name) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
