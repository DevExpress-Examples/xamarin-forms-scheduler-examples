using DevExpress.XamarinForms.Scheduler;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace CustomMonthViewProviders
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        public MainPage() {
            DevExpress.XamarinForms.Scheduler.Initializer.Init();
            InitializeComponent();
            for (int i = 0; i < 6; i++) {
                storage.AppointmentItems.Add(new AppointmentItem(AppointmentType.Normal) { Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(2), Subject = $"Item {i + 1}", LabelId = (storage.AppointmentItems.Count + 1) % storage.LabelItems.Count });
                storage.AppointmentItems.Add(new AppointmentItem(AppointmentType.Normal) { Start = DateTime.Today.AddDays(-2), End = DateTime.Today.AddDays(-1), Subject = $"Item {i + 1}", LabelId = (storage.AppointmentItems.Count + 1) % storage.LabelItems.Count });
                storage.AppointmentItems.Add(new AppointmentItem(AppointmentType.Normal) { Start = DateTime.Today.AddDays(4), End = DateTime.Today.AddDays(5), Subject = $"Item {i + 1}", LabelId = (storage.AppointmentItems.Count + 1) % storage.LabelItems.Count });
            }
        }
    }
}
