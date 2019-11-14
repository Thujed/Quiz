using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Quiz.Support.AttachedProperties.AnimationAttachedProperties.TriggerAnimationAttachedProperties
{
    public abstract class BaseAnimationTriggerAttachedProperty<Parent>
        where Parent : BaseAnimationTriggerAttachedProperty<Parent>, new()
    {
        private static Parent _instance = new Parent();

        public static bool GetTrigger(DependencyObject d) =>
            (bool)d.GetValue(TriggerProperty);

        public static void SetTrigger(DependencyObject d, bool value) =>
            d.SetValue(TriggerProperty, value);

        public static readonly DependencyProperty TriggerProperty =
            DependencyProperty.RegisterAttached(
                "Trigger",
                typeof(bool),
                typeof(BaseAnimationTriggerAttachedProperty<Parent>),
                new PropertyMetadata(
                    false,
                    null,
                    new CoerceValueCallback(OnTriggerUpdated)
                ));

        private static object OnTriggerUpdated(DependencyObject d, object baseValue) {
            BaseAnimationTriggerAttachedProperty<Parent> instance = (_instance as BaseAnimationTriggerAttachedProperty<Parent>);

            if ((bool)baseValue) {
                if (d is FrameworkElement element) {
                    instance?.GetAnimationStoryboard(d)?.Begin(element);
                } else if (d is FrameworkContentElement contentElement) {
                    instance?.GetAnimationStoryboard(d)?.Begin(contentElement);
                }
            } else {
                if (d is FrameworkElement element) {
                    instance?.GetReverseAnimationStoryboard(d)?.Begin(element);
                } else if (d is FrameworkContentElement contentElement) {
                    instance?.GetReverseAnimationStoryboard(d)?.Begin(contentElement);
                }
            }

            return baseValue;
        }

        /// <summary>
        /// Returns stroyboard with animations to start them when trigger value sets to true
        /// </summary>
        /// <param name="parameter"></param>
        public abstract Storyboard GetAnimationStoryboard(DependencyObject d);

        /// <summary>
        /// Returns stroyboard with animations to start them when trigger value sets to false
        /// </summary>
        /// <param name="parameter"></param>
        public abstract Storyboard GetReverseAnimationStoryboard(DependencyObject d);
    }
}
