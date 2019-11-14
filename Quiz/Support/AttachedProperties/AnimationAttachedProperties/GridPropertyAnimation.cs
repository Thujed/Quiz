using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Quiz.Support.CustomAnimations;

namespace Quiz.Support.AttachedProperties.AnimationAttachedProperties
{
    public class GridPropertyAnimation : BasePropertyAnimation<GridPropertyAnimation, GridLength>
    {
        public override Storyboard GetAnimationStoryboard(DependencyObject element, string propertyToAnimate, AnimationTriggerValue triggerValue, GridLength newValue, GridLength oldValue) {
            GridAnimation animation = new GridAnimation() {
                To = 
                    triggerValue == AnimationTriggerValue.Start || triggerValue == AnimationTriggerValue.Unset ? newValue : oldValue,

                From = 
                    triggerValue == AnimationTriggerValue.Start || triggerValue == AnimationTriggerValue.Unset ? oldValue : newValue,

                Duration = 
                    triggerValue == AnimationTriggerValue.Unset ? TimeSpan.FromSeconds(0.9) : TimeSpan.FromSeconds(0.18),

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
