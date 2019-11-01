using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Quiz.Supports.PropertyAnimationHandlers.TriggerAnimationHandlers
{
    public abstract class BaseSlidingShowingAnimation<Parent> : BaseAnimationTriggerAttachedProperty<Parent>
        where Parent : BaseAnimationTriggerAttachedProperty<Parent>, new()
    {

        public override Storyboard GetReverseAnimationStoryboard(DependencyObject d)
        {
            if (!(d is FrameworkElement element))
                return null;

            Thickness newMargins = GetSlidingMargins(element);
            if (newMargins == new Thickness(0)) {
                RoutedEventHandler loaded = null;
                loaded = (s, e) => {
                    SetAnimationBackValue(GetSlidingMargins(element)).Begin(element);

                    element.Loaded -= loaded;
                };

                element.Loaded += loaded;

                return null;
            } else {
                return SetAnimationBackValue(GetSlidingMargins(element));
            }
        }

        public override Storyboard GetAnimationStoryboard(DependencyObject d)
        {
            ThicknessAnimation animation = new ThicknessAnimation() {
                To = new Thickness(0),
                Duration = TimeSpan.FromSeconds(0.3)
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            Storyboard animationStoryboard = new Storyboard();
            animationStoryboard.Children.Add(animation);

            return animationStoryboard;
        }

        public Storyboard SetAnimationBackValue(Thickness margin) {
            ThicknessAnimation animation = new ThicknessAnimation() {
                To = margin,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            Storyboard animationStoryboard = new Storyboard();
            animationStoryboard.Children.Add(animation);
            return animationStoryboard;
        }


        public abstract Thickness GetSlidingMargins(FrameworkElement element);
    }

    public class SlidingUpAnimation : BaseSlidingShowingAnimation<SlidingUpAnimation>
    {
        public override Thickness GetSlidingMargins(FrameworkElement element)
        {
            double elementActualHeight = element.ActualHeight;

            return new Thickness(0, elementActualHeight, 0, -elementActualHeight);
        }
    }

    public class SlidingRightAnimation : BaseSlidingShowingAnimation<SlidingRightAnimation>
    {
        public override Thickness GetSlidingMargins(FrameworkElement element)
        {
            double elementActualWidth = element.ActualWidth;

            return new Thickness(-elementActualWidth, 0, 0, 0);
        }
    }

    public class SlidingLeftAnimation : BaseSlidingShowingAnimation<SlidingLeftAnimation>
    {
        public override Thickness GetSlidingMargins(FrameworkElement element)
        {
            double elementActualWidth = element.ActualWidth;

            return new Thickness(0, 0, -elementActualWidth, 0);
        }
    }

}
