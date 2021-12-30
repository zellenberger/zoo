using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Animals;

namespace Zoos
{
    /// <summary>
    /// The sort helper class.
    /// </summary>
    public static class SortHelper
    {
        /// <summary>
        /// The bubble sort that sorts for the name.
        /// </summary>
        /// <param name="list">The animals being sorted.</param>
        /// <param name="comparer">The comparer parameter.</param>
        /// <returns>Returns the sort result.</returns>
        public static SortResult BubbleSort(this IList list, Func<object, object, int> comparer)
        {
            int swapCounter = 0;
            int counter = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // use a for loop to loop backward through the list
            for (int i = list.Count - 1; i > 0; i--)
            {
                // loop forward as long as the loop variable is less than the outer loop variable
                for (int j = 0; j < i; j++)
                {
                    counter++;
                    if (comparer(list[j], list[j + 1]) > 0)
                    {
                        list.Swap(j, j + 1);
                        swapCounter += 1;
                    }
                }
            }

            stopwatch.Stop();

            SortResult result = new SortResult
            {
                SwapCount = swapCounter,
                Objects = list.Cast<object>().ToList(),
                CompareCount = counter,
                ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds,
            };

            return result;
        }

        /// <summary>
        /// The selection sort that sorts for the name.
        /// </summary>
        /// <param name="list">The animals being sorted.</param>
        /// <param name="comparer">The comparer parameter.</param>
        /// <returns>Returns the sort result.</returns>
        public static SortResult SelectionSort(this IList list, Func<object, object, int> comparer)
        {
            int swapCounter = 0;
            object a = null;
            int counter = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Loop forward through the list.
            for (int i = 0; i < list.Count - 1; i++)
            {
                a = list[i];
                //// loop forward as long as the loop variable is less than the outer loop variable.
                for (int j = i + 1; j < list.Count; j++)
                {
                    counter++;
                    if (comparer(list[j], a) < 0)
                    {
                        a = list[j];
                    }
                }

                if (list[i] != a)
                {
                    list.Swap(i, list.IndexOf(a));
                    swapCounter += 1;
                }
            }

            stopwatch.Stop();

            SortResult result = new SortResult
            {
                SwapCount = swapCounter,
                Objects = list.Cast<object>().ToList(),
                CompareCount = counter,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            return result;
        }

        /// <summary>
        /// Sorts the animals by insertion.
        /// </summary>
        /// <param name="list">The animals being sorted.</param>
        /// <param name="comparer">The comparer parameter.</param>
        /// <returns>Returns the sort result.</returns>
        public static SortResult InsertionSort(this IList list, Func<object, object, int> comparer)
        {
            int swapCounter = 0;
            int counter = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Loop forward through the list.
            for (int i = 1; i < list.Count; i++)
            {
                counter++;
                //// loop forward as long as the loop variable is less than the outer loop variable.
                for (int j = i; j > 0 && (comparer(list[j], list[j - 1]) < 0); j--)
                {
                    list.Swap(j, j - 1);
                    swapCounter += 1;
                }
            }

            stopwatch.Stop();

            SortResult result = new SortResult
            {
                SwapCount = swapCounter,
                Objects = list.Cast<object>().ToList(),
                CompareCount = counter,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            return result;
        }

        /// <summary>
        /// The quick sort by name method.
        /// </summary>
        /// <param name="list">The animal.</param>
        /// <param name="leftIndex">The left index.</param>
        /// <param name="rightIndex">The right index.</param>
        /// <param name="sortResult">The sort result.</param>
        /// <param name="comparer">The comparer parameter.</param>
        /// <returns>Returns done.</returns>
        public static SortResult QuickSort(this IList list, int leftIndex, int rightIndex, SortResult sortResult, Func<object, object, int> comparer)
        {
            int leftPointer = leftIndex;
            int rightPointer = rightIndex;

            // Gets the animal between the index points.
            object pivotAnimal = list[(leftIndex + rightIndex) / 2];

            bool done = false;

            while (done == false)
            {
                int pivotPostiton = list.IndexOf(pivotAnimal);

                while (comparer(list[leftPointer], pivotAnimal) < 0)
                {
                    // "Woah there's something bigger than you in this section".
                    leftPointer++;
                    sortResult.CompareCount++;
                }

                while (comparer(list[rightPointer], pivotAnimal) > 0)
                {
                    // "Woah there's something smaller than you in this section."
                    rightPointer--;
                    sortResult.CompareCount++;
                }

                if (leftPointer <= rightPointer)
                {
                    // "We have to get these animals in the right section! Let's swap them! Then let's close in on a smaller section."
                    list.Swap(leftPointer, rightPointer);
                    sortResult.SwapCount++;
                    leftPointer++;
                    rightPointer--;
                }

                if (leftPointer > rightPointer)
                {
                    // "Have we completed this section or do we need to check again?"
                    done = true;
                }
            }

            if (leftIndex < rightPointer)
            {
                // If the LEFT "section" of the list isn't sorted, sort it.
                QuickSort(list, leftIndex, rightPointer, sortResult, comparer);
            }

            if (rightIndex > leftPointer)
            {
                // If the RIGHT "section" of the list isn't sorted, sort it.
                QuickSort(list, leftPointer, rightIndex, sortResult, comparer);
            }

            sortResult.Objects = list.Cast<object>().ToList();
            return sortResult;
        }        

        /// <summary>
        /// The swap method.
        /// </summary>
        /// <param name="list">The animals being swapped.</param>
        /// <param name="index1">The first index.</param>
        /// <param name="index2">The second index.</param>
        private static void Swap(this IList list, int index1, int index2)
        {
            object animal1 = list[index1];
            object animal2 = list[index2];

            list[index2] = animal1;
            list[index1] = animal2;
        }
    }
}
