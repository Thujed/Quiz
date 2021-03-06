﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Quiz.Support.Extensions
{
    static class SizeExtensions
    {


        public static double PointsToPixels(this double wpfPoints, LengthDirection direction)
        {
            if (direction == LengthDirection.Horizontal) {
                return wpfPoints * Screen.PrimaryScreen.WorkingArea.Width / SystemParameters.WorkArea.Width;
            } else {
                return wpfPoints * Screen.PrimaryScreen.WorkingArea.Height / SystemParameters.WorkArea.Height;
            }
        }

        public static double PixelsToPoints(this double pixels, LengthDirection direction)
        {
            if (direction == LengthDirection.Horizontal) {
                return pixels * SystemParameters.WorkArea.Width / Screen.PrimaryScreen.WorkingArea.Width;
            } else {
                return pixels * SystemParameters.WorkArea.Height / Screen.PrimaryScreen.WorkingArea.Height;
            }
        }

        public enum LengthDirection
        {
            Vertical, // |
            Horizontal // ——
        }
    }
}
