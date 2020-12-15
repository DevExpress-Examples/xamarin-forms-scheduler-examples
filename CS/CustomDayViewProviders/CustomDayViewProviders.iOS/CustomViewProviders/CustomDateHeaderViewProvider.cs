using System;
using DevExpress.Xamarin.iOS.Scheduler;
using DevExpress.XamarinForms.Scheduler;
using DevExpress.XamarinForms.Scheduler.Internal;
using DevExpress.XamarinForms.Scheduler.iOS;
using Foundation;
using UIKit;

namespace CustomDayViewProviders.iOS {
    public class CustomDateHeaderViewProvider : IViewProvider {
        public void BindView(UIView view, NSObject viewInfo, ItemViewModel viewModel) {
            CustomDateHeader headerView = (CustomDateHeader)view;
            DayViewHeaderItemViewModel headerViewModel = (DayViewHeaderItemViewModel)viewModel;
            DXDayHeaderItemViewInfo headerViewInfo = (DXDayHeaderItemViewInfo)viewInfo;

            headerView.BackgroundColor = headerViewInfo.BackgroundColor;
            headerView.WeekDay.Font = headerViewInfo.WeekDayFont;
            headerView.WeekDay.TextColor = headerViewInfo.WeekDayFontColor;
            headerView.WeekDay.Text = headerViewInfo.WeekDay;
            headerView.DayNumber.Font = headerViewInfo.DayFont;
            headerView.DayNumber.TextColor = headerViewModel.IsToday ? headerViewInfo.WeekDayFontColor : headerViewInfo.DayFontColor;
            headerView.DayNumber.Text = headerViewInfo.Day;
        }

        public int GetStubColor(int logicalIndex, NSObject viewInfo, ItemViewModel viewModel) {
            return ((DayViewHeaderItemViewModel)viewModel).BackgroundColor.ToArgb();
        }

        public void RecycleView(UIView view) {
        }

        public UIView RequestView(int logicalIndex, NSObject viewInfo, ItemViewModel viewModel) {
            return new CustomDateHeader();
        }
    }
}
