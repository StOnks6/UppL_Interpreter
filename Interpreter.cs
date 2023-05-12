using System;
using System.Collections.Generic;
using System.Text;

class Interpreter
{
    private Dictionary<string, object> variables;

    public Interpreter()
    {
        variables = new Dictionary<string, object>();
    }

    public void SetVariable(string name, object value)
    {
        variables[name] = value;
    }

    public object GetVariable(string name)
    {
        if (variables.ContainsKey(name))
        {
            return variables[name];
        }
        else
        {
            throw new Exception($"Змінна '{name}' не була позначена.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Interpreter interpreter = new Interpreter();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Вітаємо в UPPl Інтерпрітаторі!");

        while (true)
        {
            Console.Write("UPPL>>> ");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                continue;
            }
            if (input == "допомога")
            {
                Console.WriteLine("Перелік команд:");
                Console.WriteLine("Надрукувати змінну, Наприклад (get x)");
                Console.WriteLine("Також ти можеш зберігати змінну");
                Console.WriteLine("Наприклад (set x 6)");
                Console.WriteLine("Ти можеш робити математичні вирази, написав наприклад 5 + 6");
            }

            if (input == "ввийти")
            {
                break;
            }

            string[] tokens = input.Split(' ');

            if (tokens[0].ToLower() == "set")
            {
                if (tokens.Length != 3)
                {
                    Console.WriteLine("Недійсна команда. Використання: встановити <ім'я_змінної> <значення>");
                    continue;
                }

                string variableName = tokens[1];
                string valueString = tokens[2];

                if (int.TryParse(valueString, out int intValue))
                {
                    interpreter.SetVariable(variableName, intValue);
                }
                else
                {
                    interpreter.SetVariable(variableName, valueString);
                }
            }
            else if (tokens[0].ToLower() == "get")
            {
                if (tokens.Length != 2)
                {
                    Console.WriteLine("Недійсна команда. Використання: отримати <ім'я_змінної>");
                    continue;
                }

                string variableName = tokens[1];

                try
                {
                    object value = interpreter.GetVariable(variableName);
                    Console.WriteLine(value);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                double result = EvaluateArithmeticExpression(input);
                Console.WriteLine("Результат: " + result);
            }
        }
    }
    static double EvaluateArithmeticExpression(string expression)
    {
        string[] tokens = expression.Split(' ');
        double result = 0;
        string operation = "";

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double number))
            {
                if (operation == "+")
                {
                    result += number;
                }
                else if (operation == "-")
                {
                    result -= number;
                }
                else if (operation == "*")
                {
                    result *= number;
                }
                else if (operation == "/")
                {
                    if (number != 0)
                    {
                        result /= number;
                    }
                    else
                    {
                        Console.WriteLine("Ділення на нуль заборонено!");
                        return result;
                    }
                }
                else
                {
                    result = number;
                }
            }
            else
            {
                operation = token;
            }
        }

        return result;
    }
}