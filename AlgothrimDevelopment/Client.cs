using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlgothrimDevelopment
{
    public class Client
    {     
        public delegate void SortingFunction(IComparable[] arrayComparables);
        public delegate IComparable[] GenerateArray(int arraySize);

        public static IComparable[] GenerateArrayValue(int size)
        {
            Random N = new Random();
            IComparable[] a = new IComparable[size];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = N.Next() / 10000;
            }
            return a;
        }

        public static string TimeAlgorithm(SortingFunction sortFunction, GenerateArray generateArray, int arraySize)
        {

            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            sortFunction(generateArray(arraySize));
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            return elapsedTime;
        }
        static void Main(string[] args)
        {
            SortAlgorithm sortAlgorithm = new SortAlgorithm();
            string header = String.Format("{0,-12}{1,8}{2,12}{3,14}\n",
                "N", "Insertion", "Selection", "Shell Sort");

            Console.WriteLine(header);
            GetValue(sortAlgorithm, GenerateParitalySorted);
           //inSortAlgorithm.Show(GenerateParitalySorted(10));
           // sortAlgorithm.Show(NonUniformDistributionsGaussian(10,10));
            Console.ReadLine();

        }
     
        private static IComparable[] GenerateParitalySorted(int size)
        {
            double precentageSorted = 0.50;//control the prcentaage sorted 0.9 = 90% on so on
            Random random = new Random();
            int startValue = random.Next(size*size);
            double interval = precentageSorted * size;
            IComparable[] values = new IComparable[size];

            for (int i = 0; i < size; i++)
            {
                if (i >= (int)interval)
                {
                    values[i] = random.Next(i);
                }
                else
                {
                     values[i] = startValue++;
                }               
            }
            return values;
        }

        private static void GetValue(SortAlgorithm inSortAlgorithm, GenerateArray generateArrayValue)
        {
            int i = 10;
            string[] timeValue = new string[3];
            while (i <= 100000)
            {
                Parallel.Invoke(() => { timeValue[0] = TimeAlgorithm(inSortAlgorithm.InsertionSort, generateArrayValue, i); },
                    () => { timeValue[1] = TimeAlgorithm(inSortAlgorithm.SelectionSort, generateArrayValue, i); },
                    () => { timeValue[2] = TimeAlgorithm(inSortAlgorithm.ShellSort, generateArrayValue, i); }
                    );

                string output = String.Format("{0,-12}{1,8}{2,12}{3,14}\n",
                    i, timeValue[0], timeValue[1], timeValue[2]);

                Console.WriteLine(output);

                i = i*10;
            }
        }

        private static IComparable[] NonUniformDistributionsGaussian(int startNumber,int arraySize)
        {
            IComparable [] arr = new IComparable[arraySize];
            Double[] intervals = { ((1.00 / 6.00) * arraySize), ((2.00 / 6.00) * arraySize),
                                     ((3.00 / 6.00) * arraySize),((4.00 / 6.00) * arraySize),
                                     ((5.00 / 6.00) * arraySize), ((6.00 / 6.00) * arraySize) };
 
            for (var i = 0; i < arraySize; i++)
            {
               
                if (i <= (int)intervals[0])
                {
                    startNumber = startNumber + 1;                   
                }

                else if ( i <= (int)intervals[1])
                {
                     startNumber = startNumber  + 2;                    
                }
                else 
                {
                     startNumber = startNumber  - 3;                     
                }
                arr[i] = startNumber;
            }
            return arr;
        }
    }
}
