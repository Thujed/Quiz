using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Quiz.Support.AttachedProperties.AnimationAttachedProperties.TriggerAnimationAttachedProperties;
using Quiz.Support.Extensions;
using Quiz.View;
using Quiz.ViewModel;

namespace Quiz.Support.AttachedProperties
{
    class ColorChangingSupportProperties
    {
        /// <summary>
        /// Instance for color picker
        /// </summary>
        private static ColorPicker _colorPicker;

        #region Properties

        public static SolidColorBrush GetColorToChange(DependencyObject d) =>
            (SolidColorBrush)d.GetValue(ColorToChangeProperty);

        public static void SetColorToChange(DependencyObject d, SolidColorBrush value) =>
            d.SetValue(ColorToChangeProperty, value);

        public static readonly DependencyProperty ColorToChangeProperty =
            DependencyProperty.RegisterAttached(
                "ColorToChange",
                typeof(SolidColorBrush),
                typeof(ColorChangingSupportProperties),
                new FrameworkPropertyMetadata(
                    default(SolidColorBrush),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnColorToChangePropertyChanged
                ));

        #endregion

        private static void OnColorToChangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement element))
                return;

            EventInfo eventField = typeof(FrameworkElement).GetEvent("MouseUp", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            FieldInfo fieldInfo = typeof(FrameworkElement).GetField(eventField.Name,
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.GetField);

            if (fieldInfo == null) {
                RoutedEventHandler loaded = null;
                loaded = (sender, eventArgs) => {
                    Point elementPosition = element.PointToScreen(new Point(0, 0));
                    _colorPicker = (Application.Current.MainWindow as MainWindow)?.ColorPickerView;
                    element.MouseUp += (s, me) => {
                        MainViewInteractionsVM interactionsVM = (Application.Current.MainWindow as MainWindow).MainWindowVM.InteractionsVM;

                        if (_colorPicker.TargetObject == d) {
                            interactionsVM.OpenColorPickerAnimationTrigger = !interactionsVM.OpenColorPickerAnimationTrigger;
                        } else {
                            (Application.Current.MainWindow as MainWindow).MainWindowVM.InteractionsVM.OpenColorPickerAnimationTrigger = true;
                        }

                        _colorPicker.TargetObject = d;

                        double elementPositionXPoints = elementPosition.X.PixelsToPoints(SizeExtensions.LengthDirection.Horizontal);
                        double elementPositionYPoints = elementPosition.Y.PixelsToPoints(SizeExtensions.LengthDirection.Vertical);

                        // Check if color picker will be out of screen
                        if (elementPositionYPoints + _colorPicker.ActualHeight > SystemParameters.PrimaryScreenHeight) {
                            _colorPicker.Margin = new Thickness(
                                elementPositionXPoints + element.ActualWidth,
                                elementPositionYPoints - _colorPicker.ActualHeight,
                                0,
                                0
                            );
                        } else {
                            _colorPicker.Margin = new Thickness(
                                elementPositionXPoints + element.ActualWidth,
                                elementPositionYPoints + element.ActualHeight,
                                0,
                                0
                            );
                        }

                        _colorPicker.BeforeBrush = GetColorToChange(d);

                        _colorPicker.PropertyChanged += (obj, args) => {
                            SetColorToChange(_colorPicker.TargetObject, _colorPicker.AfterBrush);
                        };
                    };

                    element.Loaded -= loaded;
                };
                element.Loaded += loaded;
            }
            //if (((MouseButtonEventHandler)eventField.GetAddMethod()).GetInvocationList().Length == 0) {
            //    element.MouseUp += (s, me) => {
            //        _colorPicker _colorPicker = (Application.Current.MainWindow as MainWindow)._colorPickerView;
            //        _colorPicker.BeforeBrush = GetColorToChange(d);

            //        if (_colorPicker.Visibility != Visibility.Visible)
            //            _colorPicker.Visibility = Visibility.Visible;
            //    };
            //}
        }
    }
}
