using Android.Views;
using DevExpress.XamarinForms.Scheduler.Android;
using DevExpress.XamarinForms.Scheduler;
using DevExpress.XamarinAndroid.Scheduler.Visual.Data;
using Android.Content;
using System.Collections.Generic;

namespace CustomDayViewProviders.Droid {
    public class CustomDayCellViewProvider : ICachedViewProvider {
        Queue<View> viewCache = new Queue<View>();

        public void BindView(View view, ItemViewInfo viewInfo, ItemViewModel viewModel) {
            ((CustomCell)view).ViewInfo = (CellViewInfo)viewInfo;
        }

        public View CreateNewView(int logicalIndex, ItemViewInfo viewInfo, ItemViewModel viewModel, Context context) {
            return new CustomCell(context);
        }

        public int GetStubColor(int logicalIndex, ItemViewInfo viewInfo, ItemViewModel viewModel) {
            return viewInfo.BackColor;
        }

        public void Recycle() {
            this.viewCache.Clear();
        }

        public void RecycleView(View view) {
            this.viewCache.Enqueue(view);
        }

        public View RequestViewFromCache(int logicalIndex, ItemViewInfo viewInfo, ItemViewModel viewModel) {
            if (this.viewCache.Count == 0)
                return null;
            return this.viewCache.Dequeue();
        }
    }
}