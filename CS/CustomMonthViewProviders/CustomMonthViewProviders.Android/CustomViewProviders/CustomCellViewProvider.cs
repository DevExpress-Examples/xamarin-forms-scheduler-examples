using System.Collections.Generic;
using Android.Content;
using Android.Views;
using DevExpress.XamarinAndroid.Scheduler.Visual.Data;
using DevExpress.XamarinForms.Scheduler;
using DevExpress.XamarinForms.Scheduler.Android;

namespace CustomMonthViewProviders.Droid {
    public class CustomCellViewProvider : ICachedViewProvider {
        Queue<View> cache = new Queue<View>();

        public void BindView(View view, ItemViewInfo viewInfo, ItemViewModel viewModel) {
            CustomCellView cellView = (CustomCellView)view;
            cellView.ViewInfo = (MonthCellViewInfo)viewInfo;
            cellView.ShowMoreButton = ((MonthViewCellViewModel)viewModel).ShowDownMoreButton;
        }

        public View CreateNewView(int logicalIndex, ItemViewInfo viewInfo, ItemViewModel viewModel, Context context) {
            return new CustomCellView(context);
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
