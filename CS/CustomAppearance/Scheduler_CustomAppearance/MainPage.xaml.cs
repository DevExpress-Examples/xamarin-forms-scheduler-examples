using System;
using Xamarin.Forms;
using DevExpress.XamarinForms.Scheduler;

namespace Scheduler_CustomAppearance {
    public partial class MainPage : ContentPage {
        public MainPage() {
            DevExpress.XamarinForms.Scheduler.Initializer.Init();
            InitializeComponent();
        }
    }
    class WorkWeekViewCellCustomizer : IDayViewCellCustomizer {
        public void Customize(DayViewCellViewModel cell) {
            if (cell.Interval.Start.Hour < DateTime.Now.Hour
                && cell.Interval.Start.Date == DateTime.Today) {
                cell.BackgroundColor = Color.FromHex("#fbf7e0");
            }
        }
    }
}
