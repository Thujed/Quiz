using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using Quiz.Supports.Extensions;

namespace Quiz.Supports.PropertyAnimationHandlers
{
    class SlidingShowDoubleAnimation : BasePropertyAnimation<SlidingShowDoubleAnimation, double>
    {
        public override Storyboard GetAnimationStoryboard(DependencyObject element, string propertyToAnimate, AnimationTriggerValue triggerValue, double newValue, double oldValue)
        {
            DoubleAnimation animation = new DoubleAnimation() {
                To =
                    triggerValue == AnimationTriggerValue.Start || triggerValue == AnimationTriggerValue.Unset ? element.GetElementDependencyPropertyValue<double>("ActualWidth") : oldValue,

                From =
                    triggerValue == AnimationTriggerValue.Start || triggerValue == AnimationTriggerValue.Unset ? oldValue : element.GetElementDependencyPropertyValue<double>("ActualWidth"),

                Duration =
                    triggerValue == AnimationTriggerValue.Unset ? TimeSpan.FromSeconds(0.3) : TimeSpan.FromSeconds(0.18),

                AccelerationRatio =
                    triggerValue == AnimationTriggerValue.Unset ? 0.3 : 0.7,

                EasingFunction =
                    new QuarticEase()
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath(propertyToAnimate));

            Storyboard animationStoryboard = new Storyboard();
            animationStoryboard.Children.Add(animation);

            return animationStoryboard;
        }
    }
}