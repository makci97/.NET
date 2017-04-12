using System;

namespace List
{
    internal class Program
    {
        public static class Test<T>
        {
            public static void PushIntElems(List<int> list)
            {
                // create list: front{-1, 0, 1, 2}back
                Console.WriteLine("Push {0} elems", 4);
                list.PushBack(1);
                list.PushFront(0);
                list.PushBack(2);
                list.PushFront(-1);
            }

            public static void PopElems(List<T> list)
            {
                Console.WriteLine("Front = {0}", list.Front());
                Console.WriteLine("Back = {0}", list.Back());
                Console.WriteLine("PopBack returned = {0}", list.PopBack());
                Console.WriteLine("PopFront returned = {0}", list.PopFront());
                Console.WriteLine("PopFront returned = {0}", list.PopFront());
                Console.WriteLine("PopBack returned = {0}", list.PopBack());
                Console.WriteLine("PopBack returned = {0}", list.PopBack());
                Console.WriteLine("PopFront returned = {0}", list.PopFront());
            }

            public static void Size(List<T> list)
            {
                Console.WriteLine("Size = {0}", list.Size());
            }


            public static void Enumerable(List<T> list)
            {
                var enr = list.GetEnumerator();

                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine("    Enumerable {0}:", i + 1);
                    // move and write current elem
                    try
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            Console.WriteLine("        Cur = {0}:", enr.Current);
                            Console.WriteLine("        Move:");
                            enr.MoveNext();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("        Enumerator in the end");
                    }

                    //reset
                    enr.Reset();
                    Console.WriteLine("        Enumerator in the begin");
                }

            }
        }

        public static void Main(string[] args)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("{0} testing sprint:", i + 1);


                Test<int>.Size(list);
                Test<int>.PushIntElems(list);
                Test<int>.Enumerable(list);
                Test<int>.Size(list);
                try
                {
                    Test<int>.PopElems(list);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    Console.WriteLine("Some strange thing");
                }
                Test<int>.Size(list);

            }
        }
    }
}
