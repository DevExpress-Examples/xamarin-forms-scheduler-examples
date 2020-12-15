using System;
using CoreGraphics;
using DevExpress.Xamarin.iOS.Scheduler;
using UIKit;

namespace CustomDayViewProviders.iOS {
    public class CustomCell : UIView {
        private DXCellItemViewInfo viewInfo;

        public CustomCell() {
        }

        public DXCellItemViewInfo ViewInfo {
            get { return this.viewInfo; }
            set {
                if (this.viewInfo == value)
                    return;
                this.viewInfo = value;
                BackgroundColor = this.viewInfo.BackgroundColor;
            }
        }

        public override void Draw(CGRect rect) {
            CGContext ctx = UIGraphics.GetCurrentContext();

            if (ViewInfo.LeftBorderThickness > 0) {
                ctx.SetStrokeColor(ViewInfo.LeftBorderColor.CGColor);
                ctx.SetLineWidth((nfloat)ViewInfo.LeftBorderThickness);
                ctx.BeginPath();
                ctx.MoveTo(rect.GetMinX(), rect.GetMinY());
                ctx.AddLineToPoint(rect.GetMinX(), rect.GetMaxY());
                ctx.StrokePath();
            }
            if (ViewInfo.BottomBorderThickness > 0) {
                ctx.SetStrokeColor(ViewInfo.BottomBorderColor.CGColor);
                ctx.SetLineWidth((nfloat)ViewInfo.BottomBorderThickness);
                ctx.BeginPath();
                ctx.MoveTo(rect.GetMinX(), rect.GetMaxY());
                ctx.AddLineToPoint(rect.GetMaxX(), rect.GetMaxY());
                ctx.StrokePath();
            }
        }
    }
}
