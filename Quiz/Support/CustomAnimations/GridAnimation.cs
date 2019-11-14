using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Quiz.Support.CustomAnimations
{
    class GridAnimation : AnimationTimeline
    {
        public GridAnimation()
        {
            // no-op
        }

        public GridAnimation(GridLength toValue, Duration duration)
        {
            this.To = toValue;
            this.Duration = duration;
        }

        public GridLength From
        {
            get { return (GridLength)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty FromProperty =
          DependencyProperty.Register("From", typeof(GridLength), typeof(GridAnimation));


        public GridLength To
        {
            get { return (GridLength)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(GridLength), typeof(GridAnimation));



        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(GridAnimation));


        public override Type TargetPropertyType
        {
            get { return typeof(GridLength); }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new GridAnimation();
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromValue = this.From.Value;
            double toValue = this.To.Value;

            double animationProgress = animationClock.CurrentProgress.Value;

            IEasingFunction easingFunction = EasingFunction;
            if (easingFunction != null) {
                animationProgress = easingFunction.Ease(animationProgress);
            }

            return new GridLength(From.Value + ((To.Value - From.Value) * animationProgress), this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);

            //if (fromValue > toValue) {
            //    return new GridLength((1 - animationProgress) * (fromValue - toValue) + toValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            //} else {
            //    return new GridLength(animationProgress * (toValue - fromValue) + fromValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            //}
        }
    }
}
