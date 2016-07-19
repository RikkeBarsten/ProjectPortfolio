using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.SortSearch
{
    public static class Sort
    {
        
        #region Mergesort


        // Mergesort algorithm - top down
        public static List<T> MergeSort<T>(List<T> items) where T : System.IComparable<T>
        {

            // If list contains only one item, there's nothing to sort:
            if (items.Count <= 1)
                return items;

            // Creating a list to hold the final sorted list
            List<T> result = new List<T>();

            // List to store all items less than the first item
            List<T> before = new List<T>();

            // List to store all items greater than the first item
            List<T> after = new List<T>();

            // Choosing the first item in the list as starting point
            var pivot = items[0];

            // Putting all items less than the first item in the before list
            // and all items greater than the first item in the after list
            for (int i = 1; i < items.Count; i++)
            {
                if (items[i].CompareTo(pivot) > 0)
                    after.Add(items[i]);

                else before.Add(items[i]);
            }

            // Recursively sort the before and after lists, and add them to the result
           
            result.AddRange(MergeSort(before));
            result.Add(pivot);
            result.AddRange(MergeSort(after));

            return result;
        }
        #endregion

        #region InsertionSort

        //Insertion sort algorithm
        public static void InsertionSort<T>(List<T> items) where T : System.IComparable<T>
        {
            

            //For each item in the list, starting at the second item
            for (int i = 1; i < items.Count; i++)
            {
                // Store the item in a temporary variable
                var temp = items[i];

                // Set a pointer variable to point at the item
                int pointer = i;

                // While the pointer is greater than zero, and the item is less than the item before
                while (pointer > 0 && (temp.CompareTo(items[pointer - 1]) < 0))
                {
                    // replace the item with the item before on the list
                    items[pointer] = items[pointer - 1];
                    pointer--;
                }
                // Finally - insert the original item 
                items[pointer] = temp;
            }
            
        }
    }
    #endregion

}
