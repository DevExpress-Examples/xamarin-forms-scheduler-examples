using System;
using CoreGraphics;
using UIKit;

namespace CustomMonthViewProviders.iOS {
    public class CustomAppointmentView : UIView {
        public CustomAppointmentView() {
            SubjectLabel = new UILabel();
            AddSubview(SubjectLabel);
        }

        public UILabel SubjectLabel { get; }

        public override CGSize SizeThatFits(CGSize size) {
            return SubjectLabel.SizeThatFits(size);
        }

        public override void LayoutSubviews() {
            SubjectLabel.Frame = new CGRect(0, 0, Bounds.Width, Bounds.Height);
        }
    }
}
