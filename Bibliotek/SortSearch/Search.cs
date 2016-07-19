using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.SortSearch
{
    public static class Search
    {
        public static int LinearSearch<T>(List<T> list, T itemToFind)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(itemToFind))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int BinarySearch<T>(List<T> list, T itemToFind) where T : System.IComparable
        {
            int result = DoBinary(list.ToArray(), itemToFind, 0, list.Count - 1);
            return result;

        }

        private static int DoBinary<T>(T[] array, T key, int min, int max) where T : System.IComparable
        {
            while (max >= min)
            { 
                int midpoint = findMidpoint(min, max);

                if (array[midpoint].CompareTo(key) < 0)
                {
                min = midpoint + 1;
                }
                else if (array[midpoint].CompareTo(key) > 0)
                {
                max = midpoint - 1;
                }
                else
                    return midpoint;
            }
            return -1;
           
        }

        private static int findMidpoint(int min, int max)
        {
            double midpoint = (double)min + (((double)max - (double)min) / 2);
            int mid = (int)Math.Ceiling(midpoint);

            return mid;
        }
    }
}
