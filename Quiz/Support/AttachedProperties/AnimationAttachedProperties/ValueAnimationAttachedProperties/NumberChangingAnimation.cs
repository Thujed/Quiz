using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Quiz.Support.Extensions;

namespace Quiz.Support.AttachedProperties.AnimationAttachedProperties.ValueAnimationAttachedProperties
{
    class NumberChangingAnimation : BaseValueChangingAnimationAttachProperty<NumberChangingAnimation, string>
    {
        /// <summary>
        /// Duration of animation (in milliseconds)
        /// </summary>
        private const int ANIMATION_DURATION = 5000;

        private int _toValue;
        private int _currentValue;

        private TextBlock _animatedTextBlock;

        private DispatcherTimer _valueChangingTimer = new DispatcherTimer();
        private void ValueChangingTimer_Tick(object sender, EventArgs e)
        {
            if (_currentValue >= _toValue) {
                _valueChangingTimer.Stop();
                return;
            }

            _currentValue++;

            _animatedTextBlock.Text = _currentValue.ToString();
        }

        public override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (!(d is TextBlock textBlock))
                return;

            _animatedTextBlock = textBlock;

            int newValue = Convert.ToInt32((string)e.NewValue);
            int oldValue = Convert.ToInt32((string)e.OldValue);

            _toValue = newValue;
            _currentValue = oldValue;


            _valueChangingTimer.Interval = TimeSpan.FromMilliseconds(ANIMATION_DURATION / Math.Abs(newValue - oldValue));
            _valueChangingTimer.Tick += ValueChangingTimer_Tick;
            _valueChangingTimer.Start();
        }
    }
}
