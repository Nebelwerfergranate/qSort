using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qSort
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] myInts = { 8, 6, 4, 2, 0, 9, 7, 5, 3, 1 };
            Sorter.QSortAsc(myInts);
            foreach (int value in myInts)
            {
                Console.WriteLine(value);
            }
            Console.ReadKey();
        }
    }

    static class Sorter
    {
        public static int[] QSortAsc(int[] arr)
        {
            QuickSortAscending(arr, 0, arr.Length - 1);
            return arr;
        }
        private static void QuickSortAscending(int[] arr, int left, int right)
        {
            int leftPointer = left;
            int rightPointer = right;
            int temp = 0;
            int middle = arr[(leftPointer + rightPointer) / 2];

            do
            {
                while (arr[leftPointer] < middle && leftPointer <= right)
                {
                    leftPointer++;
                }
                while (arr[rightPointer] > middle && rightPointer >= left)
                {
                    rightPointer--;
                }
                if (leftPointer <= rightPointer)
                {
                    temp = arr[leftPointer];
                    arr[leftPointer] = arr[rightPointer];
                    arr[rightPointer] = temp;
                    leftPointer++;
                    rightPointer--;
                }
            } while (leftPointer <= rightPointer);
            if (rightPointer > left)
            {
                QuickSortAscending(arr, left, rightPointer);
            }
            if (leftPointer < right)
            {
                QuickSortAscending(arr, leftPointer, right);
            }
        }
    }
}
