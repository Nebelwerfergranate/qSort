using System;
using System.Diagnostics;

namespace qSort
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int amount = 10000000;
            int[] myInts = new int[amount];

            Random rand = new Random();
            for (int i = 0; i < myInts.Length; i++)
            {
                myInts[i] = rand.Next();
            }
            int[] myInts2 = new int[amount];
            myInts.CopyTo(myInts2, 0);
            int[] myInts3 = new int[amount];
            myInts.CopyTo(myInts3, 0);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            myInts = Sorter.QSortAsc(myInts);
            stopWatch.Stop();
            PrintTime(stopWatch.Elapsed);

            stopWatch.Restart();
            Array.Sort(myInts2);
            stopWatch.Stop();
            PrintTime(stopWatch.Elapsed);
            Console.WriteLine("Массивы 1 и 2 равны: " + CompareIntArrays(myInts, myInts2));

            stopWatch.Restart();
            Sorter.QISortAsc(ref myInts3);
            stopWatch.Stop();
            PrintTime(stopWatch.Elapsed);
            Console.WriteLine("Массивы 2 и 3 равны: " + CompareIntArrays(myInts2, myInts3));
            //PrintArray(myInts);
            Console.ReadKey();
        }

        private static void PrintTime(TimeSpan ts)
        {
            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        private static bool CompareIntArrays(int[] firstArr, int[] secondArr)
        {
            if (firstArr.Length != secondArr.Length)
            {
                return false;
            }
            for (int i = 0; i < firstArr.Length; i++)
            {
                if (firstArr[i] != secondArr[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static void PrintArray(int[] arr)
        {
            foreach (int value in arr)
            {
                Console.WriteLine(value);
            }
        }
    }

    static class Sorter
    {
        //private static Random rand = new Random();
        //private static int temp = 0;
        private const int Cutoff = 6;
        public static int[] QSortAsc(int[] arr)
        {
            // Потери из-за передачи параметра по значению и его возврата составляют до 1% 0.02...0.05
            QuickSortAscending(ref arr, 0, arr.Length - 1);
            return arr;
        }
        public static void QISortAsc(ref int[] arr)
        {
            QuickSortAscendingRaw(ref arr, 0, arr.Length - 1);
            InsertSort(ref arr);
        }
        private static void QuickSortAscending(ref int[] arr, int left, int right)
        {
            int leftPointer = left;
            int rightPointer = right;
            // Первое измерение - использование статической переменной temp, второе - локальной.
            //int middle = arr[rand.Next(leftPointer, rightPointer)];
            // 5.46 ... 5.64  5.50 ... 5.55
            //int middle = arr[rand.Next(leftPointer, rightPointer)];
            // 5.44 ... 5.50  5.33 ... 5.45
            //int middle = arr[rightPointer];
            // 5.06 ... 5.20  5.02 ... 5.09

            int middle = arr[(leftPointer + rightPointer) / 2];
            int temp = 0;
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

        private static void QuickSortAscendingRaw(ref int[] arr, int left, int right)
        {
            // Ускорение относительно первого способа.
            //1) Cutoff = 4: 0.34 ... 0.45
            //2) Сutoff = 6: 0.46 ... 0.56
            //3) Cutoff = 10: 0.15 ... 17 иногда 0.51
            //4) Cutoff = 15: 0.14 ... 0.21 иногда 0.64
            //5) Cutoff = 20: 0.15 ... 0.18 иногда 0.67
            //6) Cutoff = 30: 0.34 ... 0.48
            //7) Cutoff = 50: 0.09 ... 0.17 иногда 0.26
            //8) Cutoff = 100: ~ -0.70
            int leftPointer = left;
            int rightPointer = right;
            int middle = arr[(leftPointer + rightPointer) / 2];
            int temp = 0;
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
            if (rightPointer > left + Cutoff)
            {
                QuickSortAscendingRaw(ref arr, left, rightPointer);
            }
            if (leftPointer < right - Cutoff)
            {
                QuickSortAscendingRaw(ref arr, leftPointer, right);
            }
        }

        static private void InsertSort(ref int[] arr)
        {
            int temp, i, j;
            int count = arr.Length;
            for (i = 0; i < count; i++)
            {
                temp = arr[i];
                for (j = i - 1; j >= 0 && arr[j] > temp; j--)
                {
                    arr[j + 1] = arr[j];
                }
                arr[j + 1] = temp;
            }
        }
    }
}
