using ExpressionCalculation;
using System;
using System.Diagnostics;

namespace InfixCalculator
{
   /// <summary>
   /// Главный класс программы , который содержит метод Main, с которого начинается выполнение программы
   /// </summary>
   ///Autor: Oleg Golenishev
    internal class Program
    {
        /// <summary>
        /// Главный метод программы, с которого начинается выполнение программы
        /// </summary>
        /// <param name="args">массив строк, содержащий аргументы командной строки переданные программе</param>
        private static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var reader = new ExpressionFileReader();
            string expression = null;
            if (args.Length < 1 || String.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Некорректное имя файла");
                Console.ReadKey();
                return;
            }
            try
            {
                expression = reader.ReadExpressionFromFile(args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }
            var calculator = new ExpressionCalculator();
            Console.WriteLine(expression +"="+calculator.Calculate(expression));
            watch.Stop();
            Console.WriteLine("time taken is " + watch.Elapsed.Milliseconds);
            Console.ReadKey();
        }
    }
}