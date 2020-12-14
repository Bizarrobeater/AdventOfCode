using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    static class BinarySearch
    {
        public static bool ItemExists(List<int> list, int searchTerm, out int index)
        {
            index = FindIndexSimple(list, searchTerm);
            return (index >= 0);
        }

        public static bool ItemExists<T>(List<T> list, T searchTerm, out int index) where T : IComparable<T>
        {

            index = FindIndexObject(list, searchTerm, out bool found);
            return found;
        }

        public static int FindIndexObject<T>(List<T> list, T searchTerm, out bool found) where T : IComparable<T>
        {
            int low = 0;
            int high = list.Count - 1;
            int mid = 0;

            while (low <= high)
            {
                mid = (high + low) / 2;
                if (list[mid].CompareTo(searchTerm) < 0)
                {
                    low = mid + 1;
                }
                else if (list[mid].CompareTo(searchTerm) > 0)
                {
                    high = mid - 1;
                }
                else
                {
                    found = true;
                    return mid;
                }
            }
            found = false;
            return mid;
        }

        public static int FindIndexSimple(List<int> list, int number)
        {
            int low = 0;
            int high = list.Count - 1;
            int mid;

            while (low <= high)
            {
                mid = (high + low) / 2;
                if (list[mid] < number)
                {
                    low = mid + 1;
                }
                else if (list[mid] > number)
                {
                    high = mid - 1;
                }
                else
                {
                    return mid;
                }               
            }
            return -1;
        }
    }
}
