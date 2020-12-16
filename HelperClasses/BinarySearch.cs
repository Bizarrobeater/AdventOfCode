using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    static class BinarySearch
    {
        public static bool ItemExists(List<int> list, int searchTerm, out int index)
        {
            index = FindIndex(list, searchTerm, out bool found);
            if (!found && index == list.Count && searchTerm > list[index])
            {
                index++;
            }
            return found;
        }

        public static bool ItemExists(List<long> list, long searchTerm, out int index)
        {
            index = FindIndex(list, searchTerm, out bool found);
            if (!found && searchTerm > list[index])
            {
                index++;
            }
            return found;
        }

        public static bool ItemExistsMulti(List<long> list, long searchTerm, out int index)
        {
            int listSize = list.Count;
            bool found;
            if (listSize <= 250)
            {
                index = FindIndex(list, searchTerm, out found);
            }
            else if (listSize <= 5000)
            {
                index = ExponentialSearch(list, searchTerm, listSize, out found);
            }
            else
            {
                index = InterpolationSearch(list, searchTerm, list.Count, out found);
            }
           
            if (!found && searchTerm > list[index])
            {
                index++;
            }
            return found;
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

        public static int FindIndex(List<int> list, int number, out bool found)
        {
            int low = 0;
            int high = list.Count - 1;
            int mid = 0;
            found = false;

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
                    found = true;
                    return mid;
                }
            }
            return mid;
        }

        public static int FindIndex(List<long> list, long number, out bool found)
        {
            int low = 0;
            int high = list.Count - 1;
            int mid = 0;
            found = false;

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
                    found = true;
                    return mid;
                }
            }
            return mid;
        }

        public static int ExponentialSearch(List<long> list, long searchNumber, int listSize, out bool found)
        {
            int low;
            int high = 1;
            int mid = 0;
            found = false;

            // Exponential search first
            while (high < listSize && list[high] < searchNumber)
            {
                high *= 2;
            }
            
            low = high / 2;

            if (high >= listSize)
            {
                high = (int)listSize - 1;

            }


            low = high / 2;
            // followed by standard binary search
            while (low <= high)
            {
                mid = (high + low) / 2;
                if (list[mid] < searchNumber)
                {
                    low = mid + 1;
                }
                else if (list[mid] > searchNumber)
                {
                    high = mid - 1;
                }
                else
                {
                    found = true;
                    return mid;
                }
            }
            return mid;
        }

        public static int InterpolationSearch(List<long> list, long searchNumber, int listSize, out bool found)
        {
            int low = 0;
            int high = listSize - 1;
            int mid = 0;
            found = true;
            // followed by standard binary search
            while (low <= high && searchNumber >= list[low] && searchNumber <= list[high])
            {

                mid = low + (int)((searchNumber - list[low]) * (high - low) / (list[high] - list[low]));
                
                
                if (list[mid] < searchNumber)
                {
                    low = mid + 1;
                }
                else if (list[mid] > searchNumber)
                {
                    high = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            if (searchNumber == list[low])
                return low;
            else
            {
                found = false;
                return mid;
            }
        }
    }
}
