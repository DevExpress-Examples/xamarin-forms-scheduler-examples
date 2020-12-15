using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using DevExpress.XamarinAndroid.Scheduler.Visual.Data;

namespace CustomDayViewProviders.Droid {
    public class CustomCell : View {
        CellViewInfo viewInfo;
        Paint borderPaint;
        Color backColor;

        public CustomCell(Context context)
            : base(context) {
        }

        public CellViewInfo ViewInfo {
            get { return this.viewInfo; }
            set {
                if (this.viewInfo == value)
                    return;
                this.viewInfo = value;
                this.backColor = new Color(ViewInfo.BackColor);
                this.borderPaint = new Paint(PaintFlags.AntiAlias) { Color = new Color(ViewInfo.BottomBorderColor) };
            }
        }

        protected override void OnDraw(Canvas canvas) {
            canvas.DrawColor(this.backColor);
            DrawLeftBorder(canvas);
            DrawBottomBorder(canvas);
        }

        void DrawLeftBorder(Canvas canvas) {
            int leftBorder = ViewInfo.BorderThickness[0];
            canvas.DrawRect(0, 0, leftBorder, Height, this.borderPaint);
        }

        void DrawBottomBorder(Canvas canvas) {
            int borderHeight = ViewInfo.BorderThickness[3];
            int top = Height - borderHeight;
            canvas.DrawRect(0, top, Width, top + borderHeight, this.borderPaint);
        }
    }
}
