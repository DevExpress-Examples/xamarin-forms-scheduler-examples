using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DevExpress.XamarinAndroid.Scheduler.Visual.Data;
using DevExpress.XamarinForms.Scheduler;
using DevExpress.XamarinForms.Scheduler.Android;

namespace CustomDayViewProviders.Droid {
    public class CustomDateHeaderViewProvider : IViewProvider {
        LayoutInflater inflater;

        public CustomDateHeaderViewProvider() {
            this.inflater = LayoutInflater.From(Application.Context);
        }

        public void BindView(View view, ItemViewInfo viewInfo, ItemViewModel viewModel) {
            HeaderViewInfo headerViewInfo = (HeaderViewInfo)viewInfo;
            DayViewHeaderItemViewModel headerViewModel = (DayViewHeaderItemViewModel)viewModel;
            view.SetBackgroundColor(new Color(viewInfo.BackColor));

            TextView weekDayView = view.FindViewById<TextView>(Resource.Id.tvDay);
            TextView dayNumberView = view.FindViewById<TextView>(Resource.Id.tvDayNumber);
            weekDayView.Typeface = headerViewInfo.WeekDayTextElement.Typeface;
            weekDayView.TextSize = (float)headerViewModel.WeekDayTextFontSize;
            weekDayView.SetTextColor(new Color(headerViewInfo.WeekDayTextElement.TextColor));
            weekDayView.Text = headerViewInfo.WeekDayTextElement.Text;

            dayNumberView.Typeface = headerViewInfo.DayNumberTextElement.Typeface;
            dayNumberView.TextSize = (float)headerViewModel.DayNumberTextFontSize;
            if (headerViewModel.IsToday)
                dayNumberView.SetTextColor(new Color(headerViewInfo.WeekDayTextElement.TextColor));
            else
                dayNumberView.SetTextColor(new Color(headerViewInfo.DayNumberTextElement.TextColor));
            dayNumberView.Text = headerViewInfo.DayNumberTextElement.Text;
        }

        public View CreateNewView(int logicalIndex, ItemViewInfo viewInfo, ItemViewModel viewModel, Context context) {
            return inflater.Inflate(Resource.Layout.CustomDateHeaderLayout, null);
        }

        public int GetStubColor(int logicalIndex, ItemViewInfo viewInfo, ItemViewModel viewModel) {
            return viewInfo.BackColor;
        }
    }
}