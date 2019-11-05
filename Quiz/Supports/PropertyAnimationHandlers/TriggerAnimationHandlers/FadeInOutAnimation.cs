using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Quiz.Supports.PropertyAnimationHandlers.TriggerAnimationHandlers
{
    class FadeInOutAnimation : BaseAnimationTriggerAttachedProperty<FadeInOutAnimation>
    {
        public override Storyboard GetReverseAnimationStoryboard(DependencyObject d) {
            DoubleAnimation animation = new DoubleAnimation() {
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            Storyboard animationStoryboard = new Storyboard();
            animationStoryboard.Children.Add(animation);

            return animationStoryboard;
        }

        public override Storyboard GetAnimationStoryboard(DependencyObject d) {
            DoubleAnimation animation = new DoubleAnimation() {
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.6)
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            Storyboard animationStoryboard = new Storyboard();
            animationStoryboard.Children.Add(animation);

            return animationStoryboard;
        }
    }
}
