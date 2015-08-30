using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qSort
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int amount = 100000000;
            int[] myInts = new int[amount];
            Random rand = new Random();
            for (int i = 0; i < myInts.Length; i++)
            {
                myInts[i] = rand.Next();
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Sorter.QSortAsc(myInts);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);


            //foreach (int value in myInts)
            //{
            //    Console.WriteLine(value);
            //}
            Console.ReadKey();
        }
    }

    static class Sorter
    {
        public static int[] QSortAsc(int[] arr)
        {
            QuickSortAscending(ref arr, 0, arr.Length - 1);
            return arr;
        }
        private static void QuickSortAscending(ref int[] arr, int left, int right)
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
                QuickSortAscending(ref arr, left, rightPointer);
            }
            if (leftPointer < right)
            {
                QuickSortAscending(ref arr, leftPointer, right);
            }
        }
    }
}
