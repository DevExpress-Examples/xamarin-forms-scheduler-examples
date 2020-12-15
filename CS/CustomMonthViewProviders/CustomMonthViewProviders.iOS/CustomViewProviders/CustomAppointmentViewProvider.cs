using System;
using System.Collections.Generic;
using DevExpress.Xamarin.iOS.Scheduler;
using DevExpress.XamarinForms.Scheduler;
using DevExpress.XamarinForms.Scheduler.Internal;
using DevExpress.XamarinForms.Scheduler.iOS;
using Foundation;
using UIKit;

namespace CustomMonthViewProviders.iOS {
    public class CustomAppointmentViewProvider : IViewProvider {
        Queue<UIView> cache = new Queue<UIView>();

        public void BindView(UIView view, NSObject viewInfo, ItemViewModel viewModel) {
            CustomAppointmentView appointmentView = (CustomAppointmentView)view;
            DXAppItemViewInfo appointmentViewInfo = (DXAppItemViewInfo)viewInfo;

            appointmentView.BackgroundColor = appointmentViewInfo.BackgroundColor;
            appointmentView.SubjectLabel.Text = appointmentViewInfo.Text;
            appointmentView.SubjectLabel.Font = appointmentViewInfo.TextFont;
            appointmentView.SubjectLabel.TextColor = appointmentViewInfo.TextColor;
        }

        public int GetStubColor(int logicalIndex, NSObject viewInfo, ItemViewModel viewModel) {
            return ((AppointmentViewModel)viewModel).BackgroundColor.ToArgb();
        }

        public void RecycleView(UIView view) {
            this.cache.Enqueue(view);
        }

        public UIView RequestView(int logicalIndex, NSObject viewInfo, ItemViewModel viewModel) {
            if (this.cache.Count != 0)
                return this.cache.Dequeue();
            return new CustomAppointmentView();
        }
    }
}
