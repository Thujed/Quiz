using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using Quiz.Support.CustomAnimations;

namespace Quiz.Support.AttachedProperties.AnimationAttachedProperties.TriggerAnimationAttachedProperties
{
    abstract class BaseTriggerAnimationWithProperty<Parent, Property> : BaseAnimationTriggerAttachedProperty<Parent>
        where Parent : BaseAnimationTriggerAttachedProperty<Parent>, new()
    {
        public static Property GetAnimationValue(DependencyObject d) =>
            (Property)d.GetValue(AnimationValueProperty);

        public static void SetAnimationValue(DependencyObject d, Property value) =>
            d.SetValue(AnimationValueProperty, value);

        public static readonly DependencyProperty AnimationValueProperty =
            DependencyProperty.RegisterAttached(
                "AnimationValue",
                typeof(Property),
                typeof(BaseTriggerAnimationWithProperty<Parent, Property>),
                new PropertyMetadata(
                    default(Property)
                ));

        public static Property GetReverseAnimationValue(DependencyObject d) =>
            (Property)d.GetValue(ReverseAnimationValueProperty);

        public static void SetReverseAnimationValue(DependencyObject d, Property value) =>
            d.SetValue(ReverseAnimationValueProperty, value);

        public static readonly DependencyProperty ReverseAnimationValueProperty =
            DependencyProperty.RegisterAttached(
                "ReverseAnimationValue",
                typeof(Property),
                typeof(BaseTriggerAnimationWithProperty<Parent, Property>),
                new PropertyMetadata(
                    default(Property)
                ));

        public static string GetAnimationProperty(DependencyObject d) =>
            (string)d.GetValue(AnimationPropertyProperty);

        public static void SetAnimationProperty(DependencyObject d, string value) =>
            d.SetValue(AnimationPropertyProperty, value);

        public static readonly DependencyProperty AnimationPropertyProperty =
            DependencyProperty.RegisterAttached(
                "AnimationProperty",
                typeof(string),
                typeof(BaseTriggerAnimationWithProperty<Parent, Property>),
                new PropertyMetadata(
                    "Width"
                ));
    }

    class GridLenghtPropertyAnimation : BaseTriggerAnimationWithProperty<GridLenghtPropertyAnimation, GridLength>
    {
        public override Storyboard GetReverseAnimationStoryboard(DependencyObject d) {
            GridAnimation animation = new GridAnimation() {
                To = GetReverseAnimationValue(d),
                From = GetAnimationValue(d),
                Duration = TimeSpan.FromSeconds(0.4)
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath(GetAnimationProperty(d)));

            Storyboard animationStoryboard = new Storyboard();
            animationStoryboard.Children.Add(animation);

            return animationStoryboard;
        }

        public override Storyboard GetAnimationStoryboard(DependencyObject d) {
            GridAnimation animation = new GridAnimation() {
                To = GetAnimationValue(d),
                From = GetReverseAnimationValue(d),
                Duration = TimeSpan.FromSeconds(0.3)
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath(GetAnimationProperty(d)));

            Storyboard animationStoryboard = new Storyboard();
            animationStoryboard.Children.Add(animation);

            return animationStoryboard;
        }
    }
}
