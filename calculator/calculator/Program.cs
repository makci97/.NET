using System;

namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) //Бесконечный цикл
            {
                Console.Write("Input expression : "); //Предлагаем ввести выражение
                Console.WriteLine(RPN.Calculate(Console.ReadLine())); //Считываем, и выводим результат
            }
        }
    }
}