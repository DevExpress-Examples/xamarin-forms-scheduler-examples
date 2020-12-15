using System;
using System.Collections.Generic;
using DevExpress.Xamarin.iOS.Scheduler;
using DevExpress.XamarinForms.Scheduler;
using DevExpress.XamarinForms.Scheduler.Internal;
using DevExpress.XamarinForms.Scheduler.iOS;
using Foundation;
using UIKit;

namespace CustomMonthViewProviders.iOS {
    public class CustomCellViewProvider : IViewProvider {
        Queue<UIView> cache = new Queue<UIView>();

        public void BindView(UIView view, NSObject viewInfo, ItemViewModel viewModel) {
            CustomCellView cellView = (CustomCellView)view;
            cellView.ViewInfo = (DXMonthCellItemViewInfo)viewInfo;
            MonthViewCellViewModel cellViewModel = ((MonthViewCellViewModel)viewModel);
            cellView.ShowMoreButton = cellViewModel.ShowDownMoreButton;
            cellView.CellHeaderHeight = cellViewModel.DayNumberHeight;
        }

        public int GetStubColor(int logicalIndex, NSObject viewInfo, ItemViewModel viewModel) {
            return ((CellViewModel)viewModel).BackgroundColor.ToArgb();
        }

        public void RecycleView(UIView view) {
            this.cache.Enqueue(view);
        }

        public UIView RequestView(int logicalIndex, NSObject viewInfo, ItemViewModel viewModel) {
            if (this.cache.Count != 0)
                return this.cache.Dequeue();
            return new CustomCellView();
        }
    }
}
