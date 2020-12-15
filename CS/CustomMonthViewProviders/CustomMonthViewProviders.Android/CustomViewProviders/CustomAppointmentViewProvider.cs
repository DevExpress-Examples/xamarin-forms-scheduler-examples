using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using DevExpress.XamarinAndroid.Scheduler.Visual.Data;
using DevExpress.XamarinForms.Scheduler;
using DevExpress.XamarinForms.Scheduler.Android;

namespace CustomMonthViewProviders.Droid {
    public class CustomAppointmentViewProvider : ICachedViewProvider {
        Queue<View> cache = new Queue<View>();

        public void BindView(View view, ItemViewInfo viewInfo, ItemViewModel viewModel) {
            CustomAppointmentView appointmentView = (CustomAppointmentView)view;
            AppointmentViewInfo appointmentViewInfo = (AppointmentViewInfo)viewInfo;

            appointmentView.SetBackgroundColor(new Color(viewInfo.BackColor));
            appointmentView.SubjectView.Text = appointmentViewInfo.TextElementInfo.Text;
            appointmentView.SubjectView.Typeface = appointmentViewInfo.TextElementInfo.Typeface;
            appointmentView.SubjectView.SetTextSize(ComplexUnitType.Px, appointmentViewInfo.TextElementInfo.TextSize);
            appointmentView.SubjectView.SetTextColor(new Color(appointmentViewInfo.TextElementInfo.TextColor));
        }

        public View CreateNewView(int logicalIndex, ItemViewInfo viewInfo, ItemViewModel viewModel, Context context) {
            return new CustomAppointmentView(context);
        }

        public int GetStubColor(int logicalIndex, ItemViewInfo viewInfo, ItemViewModel viewModel) {
            return viewInfo.BackColor;
        }

        public void Recycle() {
            this.cache.Clear();
        }

        public void RecycleView(View view) {
            this.cache.Enqueue(view);
        }

        public View RequestViewFromCache(int logicalIndex, ItemViewInfo viewInfo, ItemViewModel viewModel) {
            if (this.cache.Count == 0)
                return null;
            return cache.Dequeue();
        }
    }
}
