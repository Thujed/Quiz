using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Quiz.Support.AttachedProperties.AnimationAttachedProperties.ValueAnimationAttachedProperties
{
    public abstract class BaseValueChangingAnimationAttachProperty<Parent, Property>
        where Parent : BaseValueChangingAnimationAttachProperty<Parent, Property>, new()
    {
        private static Parent _instance = new Parent();

        public static Property GetValue(DependencyObject d) =>
            (Property)d.GetValue(ValueProperty);

        public static void SetValue(DependencyObject d, Property value) =>
            d.SetValue(ValueProperty, value);

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(Property),
                typeof(BaseValueChangingAnimationAttachProperty<Parent, Property>),
                new PropertyMetadata(
                    default(Property),
                    new PropertyChangedCallback(AnimationValue_OnPropertyChanged)
                ));

        private static void AnimationValue_OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement element))
                return;

            (_instance as BaseValueChangingAnimationAttachProperty<Parent, Property>)?.OnValueChanged(d, e);
        }

        /// <summary>
        /// Fired when value changed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public abstract void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
    }
}
