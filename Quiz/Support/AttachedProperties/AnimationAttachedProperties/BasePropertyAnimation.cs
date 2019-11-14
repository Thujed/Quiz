using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Quiz.Support.CustomAnimations;
using Quiz.Support.Extensions;

namespace Quiz.Support.AttachedProperties.AnimationAttachedProperties
{
    /// <summary>
    /// Class for base animation with property
    /// If trigger is unset or "False", value of <see cref="PropertyToAnimateProperty"/> chase <see cref="AnimationValueProperty"/> with animation
    /// If trigger is set or "True", value of <see cref="PropertyToAnimateProperty"/> chase <see cref="AnimationValueProperty"/> with animation 
    /// only when <see cref="AnimationTriggerProperty"/> 
    /// /// </summary>
    /// <typeparam name="Parent"></typeparam>
    /// <typeparam name="Property"></typeparam>
    public abstract class BasePropertyAnimation<Parent, Property>
        where Parent : BasePropertyAnimation<Parent, Property>, new()
    {

        protected static Parent Instance { get; set; } = new Parent();

        #region Dependency properties

        /// <summary>
        /// Gets animation value attached property
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <returns></returns>
        public static Property GetAnimationValue(DependencyObject d) =>
            (Property)d.GetValue(AnimationValueProperty);

        /// <summary>
        /// Sets animation value attached property
        /// </summary>
        /// <param name="animationPropertyProperty">The element to get the property from</param>
        /// <param name="value">New value</param>
        public static void SetAnimationValue(DependencyObject d, Property value) =>
            d.SetValue(AnimationValueProperty, value);

        /// <summary>
        /// Attached property for animation value
        /// </summary>
        public static readonly DependencyProperty AnimationValueProperty =
            DependencyProperty.RegisterAttached(
                "AnimationValue",
                typeof(Property),
                typeof(BasePropertyAnimation<Parent, Property>),
                new PropertyMetadata(
                    default(Property),
                    new PropertyChangedCallback(OnAnimationValueChanged)
                ));

        /// <summary>
        /// Gets default value attached property
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <returns></returns>
        public static Property GetDefaultValue(DependencyObject d) =>
            (Property)d.GetValue(DefaultValueProperty);

        /// <summary>
        /// Sets default value attached property
        /// </summary>
        /// <param name="animationPropertyProperty">The element to get the property from</param>
        /// <param name="value">New value</param>
        public static void SetDefaultValue(DependencyObject d, Property value) =>
            d.SetValue(DefaultValueProperty, value);

        /// <summary>
        /// Attached property for default animation value. 
        /// If this is an animation with trigger, when trigger is <see cref="AnimationTriggerValue.StartReverce"/> 
        /// thats the To value of animation
        /// </summary>
        public static readonly DependencyProperty DefaultValueProperty =
            DependencyProperty.RegisterAttached(
                "DefaultValue",
                typeof(Property),
                typeof(BasePropertyAnimation<Parent, Property>),
                new PropertyMetadata(
                    default(Property)
                ));



        /// <summary>
        /// Gets attached property with property to animation
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <returns></returns>
        public static string GetPropertyToAnimate(DependencyObject d) =>
            (string)d.GetValue(PropertyToAnimateProperty);

        /// <summary>
        /// Sets attached property with property to animation
        /// </summary>
        /// <param name="animationPropertyProperty">The element to get the property from</param>
        /// <param name="value">New value</param>
        public static void SetPropertyToAnimate(DependencyObject d, string value) =>
            d.SetValue(PropertyToAnimateProperty, value);

        /// <summary>
        /// Attached property for property to animation
        /// </summary>
        public static readonly DependencyProperty PropertyToAnimateProperty =
            DependencyProperty.RegisterAttached(
                "PropertyToAnimate",
                typeof(string),
                typeof(BasePropertyAnimation<Parent, Property>),
                new PropertyMetadata(
                    "Width"
                ));

        /// <summary>
        /// Gets attached property with animation trigger
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <returns></returns>
        public static AnimationTriggerValue GetAnimationTrigger(DependencyObject d) =>
            (AnimationTriggerValue)d.GetValue(AnimationTriggerProperty);

        /// <summary>
        /// Sets attached property with animation trigger
        /// </summary>
        /// <param name="animationPropertyProperty">The element to get the property from</param>
        /// <param name="value">New value</param>
        public static void SetAnimationTrigger(DependencyObject d, AnimationTriggerValue value) =>
            d.SetValue(AnimationTriggerProperty, value);

        /// <summary>
        /// Attached property for animation trigger
        /// </summary>
        public static readonly DependencyProperty AnimationTriggerProperty =
            DependencyProperty.RegisterAttached(
                "AnimationTrigger",
                typeof(AnimationTriggerValue),
                typeof(BasePropertyAnimation<Parent, Property>),
                new PropertyMetadata(
                    AnimationTriggerValue.Unset,
                    new PropertyChangedCallback(OnTriggerChanged)
                ));


        #endregion

        private static void OnTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Property defaultValue = GetDefaultValue(d);

            (Instance as BasePropertyAnimation<Parent, Property>)?.GetAnimationStoryboardWithOldValueAndStart(d, GetPropertyToAnimate(d), (AnimationTriggerValue)e.NewValue, GetAnimationValue(d), GetDefaultValue(d));
        }

        private static void OnAnimationValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement) && !(d is FrameworkContentElement))
                return;

            if (GetAnimationTrigger(d) != AnimationTriggerValue.Unset)
                return;

            (Instance as BasePropertyAnimation<Parent, Property>)?.GetAnimationStoryboardWithOldValueAndStart(d, GetPropertyToAnimate(d), GetAnimationTrigger(d), (Property)e.NewValue, (Property)e.OldValue);
        }

        /// <summary>
        /// Check if element is FrameworkElement or FrameworkContentElement
        /// </summary>
        /// <returns></returns>
        public void GetAnimationStoryboardWithOldValueAndStart(DependencyObject element, string propertyToAnimate, AnimationTriggerValue triggerValue, 
                                                                    Property newValue, Property oldValue)
        {
            if (element is FrameworkElement) {
                GetAnimationStoryboard(element, propertyToAnimate, triggerValue, newValue, oldValue).Begin(element as FrameworkElement);
            } else if (element is FrameworkContentElement) {
                GetAnimationStoryboard(element, propertyToAnimate, triggerValue, newValue, oldValue).Begin(element as FrameworkContentElement);
            } else {
                throw new ArgumentException("Element type must be FrameworkElement or FrameworkContentElement");
            }
        }

        /// <summary>
        /// Get stortyboard for animation
        /// </summary>
        /// <param name="propertyToAnimate"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public abstract Storyboard GetAnimationStoryboard(DependencyObject element, string propertyToAnimate, AnimationTriggerValue triggerValue, Property newValue, Property oldValue);
    }

    public enum AnimationTriggerValue
    {
        StartReverce = 0,
        Start = 1,
        Unset = 2
    }
}
