using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp3;
namespace App
{
    class program
    {
        public static void Main(string[] args)
        {
            var MDS = new MyDataStructure<int>();
            MDS.array = new int[]{ 4 , 5 ,6 ,7 ,2 ,1 , 21, 11 , 15 ,3 , 4 ,5 };
            var Filter = link.Where(MDS, value => value > 4);
            var Filter2 = link.Take(Filter, 5);
            
            var EE = Filter2.GetEnumerator();
            while (EE.MoveNext())
            {
                Console.Write($"{ EE.Current} ,"); //    5, 6 ,7 ,21 , 11 ,
            }       
        }
    }
    class MyDataStructure<T> : IEnumerable<T>
    {
        public T[] array;
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
                if (index < MDS.array.Length - 1 ) 
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


}