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
            int[] myInts = { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };
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
            QuickSortAscending(ref arr, 0, arr.Length-1);
            return arr;
        }
        private static void QuickSortAscending(ref int[] arr, int left, int right)
        {
            int currentLeft = left;
            int currentRight = right;
            int temp = 0;
            int middle = arr[(currentLeft + currentRight) / 2];

            do
            {
                while (arr[currentLeft] < middle)
                {
                    currentLeft++;
                }
                while (arr[currentRight] > middle)
                {
                    currentRight--;
                }
                if (currentLeft <= currentRight)
                {
                    temp = arr[currentLeft];
                    arr[currentLeft] = arr[currentRight];
                    arr[currentRight] = temp;
                    currentLeft++;
                    currentRight--;
                }
            } while (currentLeft <= currentRight);
            if (currentRight > left)
            {
                QuickSortAscending(ref arr, left, currentRight);
            }
            if (currentLeft < right)
            {
                QuickSortAscending(ref arr, currentLeft, right);
            }
        }
    }
}
