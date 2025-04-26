using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleApp3;
namespace App
{
    class program
    {
        class MyDataStructure<T> : IEnumerable<T>
        {
            public T[] array;

            public MyDataStructure(T[]  arr)
            {
                array =arr ;
            }
            public IEnumerator<T> GetEnumerator()
            {
                return new MDSEnumerator(this);
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public class MDSEnumerator : IEnumerator<T>
            {
                int index = -1;
                MyDataStructure<T> MDS;
                public MDSEnumerator(MyDataStructure<T> MDS)
                {
                    this.MDS = MDS;
                }
                public T Current
                {
                    get
                    {
                        if (index >= 0 && index < MDS.array.Length)
                        {
                            return MDS.array[index];
                        }
                        throw new InvalidOperationException("Enumerator is in an invalid state.");
                    }
                }
                object IEnumerator.Current => Current;

                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    if (index < MDS.array.Length - 1)
                    {
                        index++;
                        return true;
                    }
                    return false;
                }

                public void Reset()
                {
                    index = -1;
                }
            }
        }
        public static void Main(string[] args)
        {
            var MyDS = new MyDataStructure<int>(new int[] { 4, 5, 6, 7, 2, 1, 21, 11, 15, 3, 4, 5 });

            var Filter = MyDS.Where(value => value > 4).Take(4);
            
            var EE = Filter.GetEnumerator();
            while (EE.MoveNext())
            {
                Console.Write($"{ EE.Current} ,"); // 5, 6 ,7 ,21 ,
            }       
            Console.WriteLine();
            var MyDS2 = new MyDataStructure<string>(new string[] { "apple", "banana", "cherry", "date", "fig", "grape" });


            var Filter2 = MyDS2.Where(value => value.EndsWith("e")).Take(3);

            foreach (var item in Filter2)
            {
                Console.Write($"{item} ,"); // apple, date, grape
            }
        }
    }
    


}