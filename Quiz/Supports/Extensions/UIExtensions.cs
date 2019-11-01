﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Quiz.Supports.Extensions
{
    static class UIExtensions
    {
        public static List<T> GetAllUIElements<T>(this DependencyObject obj, int level = 1)
            where T : DependencyObject
        {

            List<T> elemets = new List<T>(); 

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child == null)
                    continue;

                if (child is Decorator || child is Panel || child is ContentControl) {
                    elemets.AddRange(GetAllUIElements<T>(child, level++));
                } else if (child is T) {
                    elemets.Add(child as T);
                }
            }

            return elemets;
        } 

    }
}
