namespace List
{
    internal class Program
    {
        public static class Test<T>
        {
            public static void PushIntElems(List<int> list)
            {
                // create list: front{-1, 0, 1, 2}back
                System.Console.WriteLine("Push {0} elems", 4);
                list.PushBack(1);
                list.PushFront(0);
                list.PushBack(2);
                list.PushFront(-1);
            }

            public static void PopElems(List<T> list)
            {
                System.Console.WriteLine("Front = {0}", list.Front());
                System.Console.WriteLine("Back = {0}", list.Back());
                System.Console.WriteLine("PopBack returned = {0}", list.PopBack());
                System.Console.WriteLine("PopFront returned = {0}", list.PopFront());
                System.Console.WriteLine("PopFront returned = {0}", list.PopFront());
                System.Console.WriteLine("PopBack returned = {0}", list.PopBack());
                System.Console.WriteLine("PopBack returned = {0}", list.PopBack());
                System.Console.WriteLine("PopFront returned = {0}", list.PopFront());
            }

            public static void Size(List<T> list)
            {
                System.Console.WriteLine("Size = {0}", list.Size());
            }

        }

        public static void Main(string[] args)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < 2; i++)
            {
                System.Console.WriteLine("{0} testing sprint:", i + 1);


                Test<int>.Size(list);
                Test<int>.PushIntElems(list);
                Test<int>.Size(list);
                try
                {
                    Test<int>.PopElems(list);
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine(e);
                }
                finally
                {
                    System.Console.WriteLine("Some strange thing");
                }
                Test<int>.Size(list);
            }
        }
    }
}
