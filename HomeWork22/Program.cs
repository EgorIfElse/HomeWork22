
namespace HomeWork22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int max = int.MinValue;


            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, object> func2 = new Func<Task<int[]>, object>(SumNumbers);
            Task<object> task2 = task1.ContinueWith<object>(func2);

            Func<Task<int[]>, object> func3 = new Func<Task<int[]>, object>(MaxNumbers);
            Task<object> task3 = task1.ContinueWith<object>(func3);

            //Action<Task<int[]>, Task<object>, Task<object>> action = new Action<Task<int[]>, Task<object>, Task<object>>(PrintArray);
            Task task4 = task3.ContinueWith(x => PrintArray(task1,task2,task3));
            task1.Start();
            task4.Wait();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 50);
            }
            return array;
        }

        static object SumNumbers(Task<int[]> task1)
        {
            int[] array = task1.Result;
            int summ = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                summ += array[i];
            }
            return summ;

        }

        static object MaxNumbers(Task<int[]> task1)
        {
            int[] array = task1.Result;

            int max = int.MinValue;
            for (int i = 0; i < array.Count(); i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }


            }
            return max;

        }
        static void PrintArray(Task<int[]> task1, Task<object> task2, Task<object> task3)
        {
            int summ = (int)task2.Result;
            int max = (int)task3.Result;
            int[] array = task1.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                Console.WriteLine($" {array[i]}");
            }
            Console.WriteLine(summ);
            Console.WriteLine(max);
        }

    }
}
    
