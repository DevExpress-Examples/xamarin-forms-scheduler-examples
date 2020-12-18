using System;
using System.Collections.ObjectModel;

namespace Scheduler_DarkTheme {
    public class MedicalAppointment {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public int LabelId { get; set; }
        public string Location { get; set; }
    }

    public class ReceptionDeskData {
        public static DateTime BaseDate = DateTime.Today;

        public static string[] PatientNames = { "Andrew Glover", "Mark Oliver",
                                                "Taylor Riley", "Addison Davis",
                                                "Benjamin Hughes", "Lucas Smith",
                                                "Robert King", "Laura Callahan",
                                                "Miguel Simmons", "Isabella Carter",
                                                "Andrew Fuller", "Madeleine Russell",
                                                "Steven Buchanan", "Nancy Davolio",
                                                "Michael Suyama", "Margaret Peacock",
                                                "Janet Leverling", "Ariana Alexander",
                                                "Brad Farkus", "Bart Arnaz",
                                                "Arnie Schwartz", "Billy Zimmer"};

        static Random rnd = new Random();

        void CreateMedicalAppointments() {
            int appointmentId = 1;
            int patientIndex = 0;
            DateTime start;
            TimeSpan duration;
            ObservableCollection<MedicalAppointment> result =
                                                     new ObservableCollection<MedicalAppointment>();
            for (int i = -20; i < 20; i++)
                for (int j = 0; j < 7; j++) {
                    int room = rnd.Next(1, 100);
                    start = BaseDate.AddDays(i).AddHours(rnd.Next(8, 17)).AddMinutes(rnd.Next(0, 40));
                    duration = TimeSpan.FromMinutes(rnd.Next(20, 30));
                    result.Add(CreateMedicAppointment(appointmentId, PatientNames[patientIndex],
                                                    start, duration, room));
                    appointmentId++;
                    patientIndex++;
                    if (patientIndex >= PatientNames.Length - 1)
                        patientIndex = 1;
                }
            MedicalAppointments = result;
        }

        MedicalAppointment CreateMedicAppointment(int appointmentId, string patientName,
                                                    DateTime start, TimeSpan duration, int room) {
            MedicalAppointment medicalAppointment = new MedicalAppointment();
            medicalAppointment.Id = appointmentId;
            medicalAppointment.StartTime = start;
            medicalAppointment.EndTime = start.Add(duration);
            medicalAppointment.Subject = patientName;
            medicalAppointment.LabelId = rnd.Next(1, 10);
            medicalAppointment.Location = String.Format("{0}", room);
            return medicalAppointment;
        }

        public ObservableCollection<MedicalAppointment> MedicalAppointments { get; private set; }

        public ReceptionDeskData() {
            CreateMedicalAppointments();
        }
    }
}
