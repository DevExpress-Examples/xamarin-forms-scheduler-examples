using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using DevExpress.XamarinAndroid.Scheduler.Visual.Data;

namespace CustomMonthViewProviders.Droid {
    public class CustomCellView : ViewGroup {
        View childView;
        LinearLayout headerLayout;
        TextView dateTextView;
        ImageView moreButtonImgView;
        MonthCellViewInfo viewInfo;
        Paint borderPaint;
        Color backColor;
        bool showMoreButton;

        public CustomCellView(Context context)
            : base(context) {
            SetWillNotDraw(false);
            this.childView = LayoutInflater.From(context).Inflate(Resource.Layout.CustomCellLayout, null);
            AddView(this.childView);
            this.headerLayout = this.childView.FindViewById<LinearLayout>(Resource.Id.headerLayout);
            this.dateTextView = this.childView.FindViewById<TextView>(Resource.Id.tvDate);
            this.moreButtonImgView = this.childView.FindViewById<ImageView>(Resource.Id.imgMoreButton);
        }

        public MonthCellViewInfo ViewInfo {
            get { return this.viewInfo; }
            set {
                if (this.viewInfo == value)
                    return;
                this.viewInfo = value;
                Update();
            }
        }

        public bool ShowMoreButton {
            get { return showMoreButton; }
            set {
                if (this.showMoreButton == value)
                    return;
                this.showMoreButton = value;
                this.moreButtonImgView.Visibility = this.showMoreButton ? ViewStates.Visible : ViewStates.Invisible;
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
            this.childView.Measure(widthMeasureSpec, heightMeasureSpec);
            SetMeasuredDimension(this.childView.MeasuredWidth, this.childView.MeasuredHeight);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b) {
            int left = ViewInfo.BorderThickness[0];
            int top = ViewInfo.BorderThickness[1];
            int right = r - l - ViewInfo.BorderThickness[2];
            int bottom = b - t - ViewInfo.BorderThickness[3];
            this.childView.Layout(left, top, right, bottom);
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

        void Update() {
            this.backColor = new Color(ViewInfo.BackColor);
            this.borderPaint = new Paint(PaintFlags.AntiAlias) { Color = new Color(ViewInfo.BottomBorderColor) };
            this.headerLayout.SetBackgroundColor(new Color(ViewInfo.AlternateBackgroundColor));
            this.dateTextView.Typeface = ViewInfo.DayNumberTextElement.Typeface;
            this.dateTextView.SetTextSize(ComplexUnitType.Px, ViewInfo.DayNumberTextElement.TextSize);
            this.dateTextView.SetTextColor(new Color(ViewInfo.DayNumberTextElement.TextColor));
            this.dateTextView.Text = ViewInfo.DayNumberTextElement.Text;
        }
    }
}
