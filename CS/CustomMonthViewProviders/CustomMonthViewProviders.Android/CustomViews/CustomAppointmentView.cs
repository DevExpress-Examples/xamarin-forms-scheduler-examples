using System;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace CustomMonthViewProviders.Droid {
    public class CustomAppointmentView : ViewGroup {
        public CustomAppointmentView(Context context) :
            base(context) {
            SubjectView = new TextView(context);
            AddView(SubjectView);
        }

        public TextView SubjectView { get; }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
            SubjectView.Measure(widthMeasureSpec, heightMeasureSpec);
            SetMeasuredDimension(SubjectView.MeasuredWidth, SubjectView.MeasuredHeight);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b) {
            SubjectView.Layout(4, 4, Math.Min(r - l - 8, SubjectView.MeasuredWidth), Math.Min(b - t - 8, SubjectView.MeasuredHeight));
        }
    }
}
