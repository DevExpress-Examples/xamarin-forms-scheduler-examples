using System;
using System.Collections.Generic;
using DevExpress.Xamarin.iOS.Scheduler;
using DevExpress.XamarinForms.Scheduler;
using DevExpress.XamarinForms.Scheduler.Internal;
using DevExpress.XamarinForms.Scheduler.iOS;
using Foundation;
using UIKit;

namespace CustomDayViewProviders.iOS {
    public class CustomCellViewProvider : IViewProvider {
        Queue<UIView> viewCache = new Queue<UIView>();

        public void BindView(UIView view, NSObject viewInfo, ItemViewModel viewModel) {
            ((CustomCell)view).ViewInfo = (DXCellItemViewInfo)viewInfo;
        }

        public int GetStubColor(int logicalIndex, NSObject viewInfo, ItemViewModel viewModel) {
            return ((DayViewCellViewModel)viewModel).BackgroundColor.ToArgb();
        }

        public void RecycleView(UIView view) {
            this.viewCache.Enqueue(view);
        }

        public UIView RequestView(int logicalIndex, NSObject viewInfo, ItemViewModel viewModel) {
            if (this.viewCache.Count != 0)
                return this.viewCache.Dequeue();
            return new CustomCell();
        }
    }
}
