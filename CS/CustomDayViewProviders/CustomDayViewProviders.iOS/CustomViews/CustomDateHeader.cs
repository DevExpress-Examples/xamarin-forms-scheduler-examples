using System;
using CoreGraphics;
using UIKit;

namespace CustomDayViewProviders.iOS {
    public class CustomDateHeader : UIView {
        CGSize weekDaySize;
        CGSize dayNumberSize;

        public CustomDateHeader() {
            AddSubview(WeekDay = new UILabel());
            AddSubview(DayNumber = new UILabel());
        }

        public UILabel WeekDay { get; }
        public UILabel DayNumber { get; }

        public override CGSize SizeThatFits(CGSize size) {
            this.weekDaySize = WeekDay.SizeThatFits(size);
            this.dayNumberSize = DayNumber.SizeThatFits(size);
            return new CGSize(Math.Max(this.weekDaySize.Width, this.dayNumberSize.Width), this.weekDaySize.Height + this.dayNumberSize.Height);
        }

        public override void LayoutSubviews() {
            WeekDay.Frame = new CGRect(0, 0, Bounds.Width, this.weekDaySize.Height);
            DayNumber.Frame = new CGRect(0, this.weekDaySize.Height, Bounds.Width, Bounds.Height - this.weekDaySize.Height);
            SetNeedsDisplay();
        }
    }
}
