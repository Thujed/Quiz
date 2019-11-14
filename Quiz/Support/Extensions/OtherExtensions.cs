using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Quiz.ViewModel;

namespace Quiz.Support.Extensions
{
    static class OtherExtensions
    {
        /// <summary>
        /// Set event handler to each OnPlayerSelected event of player data in collection
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void LinkActionToPlayerDataList(this ObservableCollection<PlayerDataVM> collection, Action<PlayerDataVM> action) {
            foreach (PlayerDataVM player in collection) {
                player.OnPlayerSelected += action;
            }
        }

        /// <summary>
        /// Add the element to collection and return colection to multiple adds
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="newElement"></param>
        /// <returns></returns>
        public static ObservableCollection<T> FlickAdd<T>(this ObservableCollection<T> collection, T newElement)
        {
            collection.Add(newElement);
            return collection;
        }

        public static Color ToColor(this int colorInt)
        {
            return Color.FromRgb(
                (byte)(colorInt >> 16),
                (byte)(~(0xFF << 8) & colorInt >> 8),
                (byte)(~(0xFFFF << 8) & colorInt)
            );
        } 

        public static int ToInt(this Color color)
        {
            int result = color.B;
            result |= color.G << 8;
            result |= color.R << 16;

            return result;
        }

    }
}
