using System;
using System.Collections.Generic;


namespace calculator
{
    class RPN
    {
        // Максимально допустимая длина названия вычислямых функций
        private const int MAX_LEN_FUNC_NAME = 4;

        //Метод Calculate принимает выражение в виде строки и возвращает результат, в своей работе использует другие методы класса
        //"Входной" метод класса
        static public double Calculate(string input)
        {
            SortedSet<char> variableSet = new SortedSet<char>();
            string output = GetExpression(input, variableSet); //Преобразовываем выражение в постфиксную запись
            SortedDictionary<char, double> variableValues = GetAllVariables(variableSet);
            return Counting(output, variableValues); //Решаем полученное выражение
        }

        //Метод, преобразующий входную строку с выражением в постфиксную запись
        static private string GetExpression(string input, SortedSet<char> variableSet)
        {
            string output = string.Empty; //Строка для хранения выражения
            Stack<string> operStack = new Stack<string>(); //Стек для хранения операторов
            bool mayUnary = true;   // Может ли оператор быть унарным

            for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
            {
                //Разделители пропускаем
                if (IsDelimeter(input[i]))
                    continue; //Переходим к следующему символу

                //Если символ - цифра, то считываем все число
                if (Char.IsDigit(input[i])) //Если цифра
                {
                    //Читаем до разделителя или оператора, что бы получить число
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]) && !Char.IsLetter(input[i]))
                    {
                        output += input[i]; //Добавляем каждую цифру числа к нашей строке
                        i++; //Переходим к следующему символу

                        if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }

                    output += " "; //Дописываем после числа пробел в строку с выражением
                    i--; //Возвращаемся на один символ назад, к символу перед разделителем
                    mayUnary = false;
                }
                //Если символ - оператор
                else if (IsOperator(input[i])) //Если оператор
                {
                    if (input[i] == '(') //Если символ - открывающая скобка
                    {
                        operStack.Push(input[i].ToString()); //Записываем её в стек
                        mayUnary = true;
                    }
                    else if (input[i] == ')') //Если символ - закрывающая скобка
                    {
                        //Выписываем все операторы до открывающей скобки в строку
                        string s = operStack.Pop();

                        while (s != "(")
                        {
                            output += s + ' ';
                            s = operStack.Pop();
                        }
                        mayUnary = false;
                    }
                    else //Если любой другой оператор
                    {
                        if (operStack.Count > 0) //Если в стеке есть элементы
                            if (GetPriority(input[i].ToString()) <= GetPriority(operStack.Peek())
                            ) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                                output += operStack.Pop() +
                                          " "; //То добавляем последний оператор из стека в строку с выражением

                        //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека
                        if (mayUnary && IsUnary(input[i]))
                        {
                            operStack.Push("!" + input[i]);
                        }
                        else
                        {
                            operStack.Push(input[i].ToString());
                        }
                        mayUnary = true;
                    }
                }
                // Если начало названия функции
                else if (IsFunction(input[i]))
                {
                    // Подстрока, в которой может содержаться название функции
                    string pie = input.Substring(i, Math.Min(MAX_LEN_FUNC_NAME, input.Length - i));
                    string funcName;

                    if (pie.Contains("Sin"))
                    {
                        funcName = "Sin";
                    }
                    else if (pie.Contains("Cos"))
                    {
                        funcName = "Cos";
                    }
                    else if (pie.Contains("Tg"))
                    {
                        funcName = "Tg";
                    }
                    else if (pie.Contains("Ctg"))
                    {
                        funcName = "Ctg";
                    }
                    else if (pie.Contains("Sqrt"))
                    {
                        funcName = "Sqrt";
                    }
                    else
                    {
                        Console.WriteLine("Unknown function!");
                        break;
                    }

                    if (operStack.Count > 0) //Если в стеке есть элементы
                        if (GetPriority(funcName) <= GetPriority(operStack.Peek())
                        ) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                            output += operStack.Pop() +
                                      " "; //То добавляем последний оператор из стека в строку с выражением

                    //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека
                    operStack.Push(funcName);

                    mayUnary = true;
                    i += funcName.Length - 1;
                }
                // Если переменная
                else if (Char.IsLower(input[i]))
                {
                    variableSet.Add(input[i]);
                    output += input[i]; // "Число" к нашей строке
                    output += " "; // Дописываем после числа пробел в строку с выражением

                    mayUnary = false;
                }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; //Возвращаем выражение в постфиксной записи
        }


        //Метод, получающий значения переменных
        static private SortedDictionary<char, double> GetAllVariables(SortedSet<char> variableSet)
        {
            SortedDictionary<char, double> variableValues = new SortedDictionary<char, double>();
            foreach (var v in variableSet)
            {
                Console.Write("Variable " + v.ToString() + " : ");
                var value = double.Parse(Console.ReadLine());
                variableValues[v] = value;
            }

            return variableValues;
        }


        //Метод, вычисляющий значение выражения, уже преобразованного в постфиксную запись
        static private double Counting(string input, SortedDictionary<char, double> variableValues)
        {
            Console.WriteLine(input);

            double result = 0; //Результат
            Stack<double> temp = new Stack<double>(); //Временный стек для решения

            for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            {
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                if (Char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]) && !Char.IsLetter(input[i])) //Пока не разделитель
                    {
                        a += input[i]; //Добавляем
                        i++;
                        if (i == input.Length) break;
                    }

                    temp.Push(double.Parse(a)); //Записываем в стек
                    i--;
                }
                else if (IsOperator(input[i])) //Если символ - оператор
                {
                    if (input[i] == '!')
                    {
                        // Унарный оператор
                        if (i < input.Length)
                        {
                            ++i;
                            //Берем последнее значение из стека
                            double a = temp.Pop();

                            switch (input[i]) //И производим над ними действие, согласно оператору
                            {
                                case '+':
                                    result = a;
                                    break;
                                case '-':
                                    result = -a;
                                    break;
                            }
                            temp.Push(result); //Результат вычисления записываем обратно в стек
                        }
                        else
                        {
                            Console.WriteLine("assert");    // я не понял как подключить assert
                            break;
                        }
                    }
                    else
                    {
                        // Бинарный оператор
                        //Берем два последних значения из стека
                        double a = temp.Pop();
                        double b = temp.Pop();

                        switch (input[i]) //И производим над ними действие, согласно оператору
                        {
                            case '+':
                                result = b + a;
                                break;
                            case '-':
                                result = b - a;
                                break;
                            case '*':
                                result = b * a;
                                break;
                            case '/':
                                result = b / a;
                                break;
                            case '^':
                                result = Math.Pow(b, a);
                                break;
                        }

                        temp.Push(result); //Результат вычисления записываем обратно в стек
                    }
                }
                else if (IsFunction(input[i])) //Если символ - начало имени функции
                {
                    // Функция
                    // Берем последнее значение из стека
                    double a = temp.Pop();
                    // Подстрока, в которой может содержаться название функции
                    string pie = input.Substring(i, Math.Min(MAX_LEN_FUNC_NAME, input.Length - i - 1));
                    string funcName = "";

                    if (pie.Contains("Sin"))
                    {
                        funcName = "Sin";
                        result = Math.Sin(a);
                    }
                    else if (pie.Contains("Cos"))
                    {
                        funcName = "Cos";
                        result = Math.Cos(a);
                    }
                    else if (pie.Contains("Tg"))
                    {
                        funcName = "Tg";
                        result = Math.Tan(a);
                    }
                    else if (pie.Contains("Ctg"))
                    {
                        funcName = "Ctg";
                        result = 1.0/Math.Tan(a);
                    }
                    else if (pie.Contains("Sqrt"))
                    {
                        funcName = "Sqrt";
                        result = Math.Sqrt(a);
                    }

                    i += funcName.Length - 1;

                    temp.Push(result); //Результат вычисления записываем обратно в стек
                }
                // Если переменная
                else if (Char.IsLower(input[i]))
                {
                    temp.Push(variableValues[input[i]]); //Записываем в стек
                }
            }
            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }



        //Метод возвращает true, если проверяемый символ - разделитель ("пробел" или "равно")
        static private bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
            {
                return true;
            }

            return false;
        }

        //Метод возвращает true, если проверяемый символ - оператор
        static private bool IsOperator(char с)
        {
            if (("+-/*()!^".IndexOf(с) != -1))
                return true;
            return false;
        }

        //Метод возвращает true, если проверяемый символ может быть унарным оператором
        static private bool IsUnary(char с)
        {
            if (("+-".IndexOf(с) != -1))
                return true;
            return false;
        }

        //Метод возвращает true, если проверяемый символ может быть началом названия функции
        static private bool IsFunction(char с)
        {
            // Sin, Cos, Tg, Ctg, Sqrt
            if (("SCT".IndexOf(с) != -1))
                return true;
            return false;
        }

        //Метод возвращает приоритет оператора
        static private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(": return 0;
                case ")": return 1;
                case "+": return 2;
                case "-": return 3;
                case "*": return 4;
                case "/": return 4;
                case "!+": return 5; // унарный оператор
                case "!-": return 5; // унарный оператор
                case "^": return 6;
                case "Sin": return 7;
                case "Cos": return 7;
                case "Tg": return 7;
                case "Ctg": return 7;
                case "Sqrt": return 7;
                default: return 99;
            }
        }

    }
}