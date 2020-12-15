using System;
using CoreGraphics;
using DevExpress.Xamarin.iOS.Scheduler;
using UIKit;

namespace CustomMonthViewProviders.iOS {
    public class CustomCellView : UIView {
        UILabel dateLabel;
        UIImageView moreButtonImgView;
        DXMonthCellItemViewInfo viewInfo;
        bool showMoreButton;
        double imgWidth = 10;
        double imgHeight = 8;

        public CustomCellView() {
            this.dateLabel = new UILabel();
            AddSubview(this.dateLabel);
            this.moreButtonImgView = new UIImageView(UIImage.GetSystemImage("arrowtriangle.down"));
            AddSubview(this.moreButtonImgView);
        }

        public DXMonthCellItemViewInfo ViewInfo {
            get { return this.viewInfo; }
            set {
                if (this.viewInfo == value)
                    return;
                this.viewInfo = value;
                Update();
            }
        }

        public bool ShowMoreButton {
            get { return this.showMoreButton; }
            set {
                if (this.showMoreButton == value)
                    return;
                this.showMoreButton = value;
                this.moreButtonImgView.Hidden = !this.showMoreButton;
            }
        }

        public double CellHeaderHeight { get; set; }

        public override CGSize SizeThatFits(CGSize size) {
            CGSize dateLabelSize = this.dateLabel.SizeThatFits(size);
            CGSize moreButtonSize = CGSize.Empty;
            if (ShowMoreButton)
                moreButtonSize = this.moreButtonImgView.SizeThatFits(size);
            return new CGSize(Math.Max(dateLabelSize.Width, moreButtonSize.Width), dateLabelSize.Height + moreButtonSize.Height);
        }

        public override void LayoutSubviews() {
            this.dateLabel.Frame = new CGRect(ViewInfo.LeftBorderThickness, 0, Bounds.Width - ViewInfo.LeftBorderThickness, CellHeaderHeight);
            this.moreButtonImgView.Frame = new CGRect(Bounds.Width - this.imgWidth - 1, (CellHeaderHeight - this.imgHeight) / 2, this.imgWidth, this.imgHeight);
        }

        public override void Draw(CGRect rect) {
            CGContext ctx = UIGraphics.GetCurrentContext();
            DrawLeftBorder(ctx, rect);
            DrawBottomBorder(ctx, rect);
        }

        void Update() {
            this.BackgroundColor = ViewInfo.BackgroundColor;
            this.dateLabel.BackgroundColor = ViewInfo.TextBackColor;
            this.dateLabel.Font = ViewInfo.Font;
            this.dateLabel.TextColor = ViewInfo.TextColor;
            this.dateLabel.Text = ViewInfo.Text;
        }

        void DrawLeftBorder(CGContext ctx, CGRect rect) {
            if (ViewInfo.LeftBorderThickness <= 0)
                return;
            ctx.SetStrokeColor(ViewInfo.LeftBorderColor.CGColor);
            ctx.SetLineWidth((nfloat)ViewInfo.LeftBorderThickness);
            ctx.BeginPath();
            ctx.MoveTo(rect.GetMinX(), rect.GetMinY());
            ctx.AddLineToPoint(rect.GetMinX(), rect.GetMaxY());
            ctx.StrokePath();
        }

        void DrawBottomBorder(CGContext ctx, CGRect rect) {
            if (ViewInfo.BottomBorderThickness <= 0)
                return;
            ctx.SetStrokeColor(ViewInfo.BottomBorderColor.CGColor);
            ctx.SetLineWidth((nfloat)ViewInfo.BottomBorderThickness);
            ctx.BeginPath();
            ctx.MoveTo(rect.GetMinX(), rect.GetMaxY());
            ctx.AddLineToPoint(rect.GetMaxX(), rect.GetMaxY());
            ctx.StrokePath();
        }
    }
}
