using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class BinarySearch
    {
        List<int> dataList;
        
        public BinarySearch(List<int> list)
        {
            dataList = list;
        }

        public int FindIndex(int number)
        {
            int low = 0;
            int high = dataList.Count - 1;
            int mid;

            while (low <= high)
            {
                mid = (high + low) / 2;
                if (dataList[mid] < number)
                {
                    low = mid + 1;
                }
                else if (dataList[mid] > number)
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
