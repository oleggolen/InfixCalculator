using System;

namespace InfixCalculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var reader = new ExpressionFileReader();
            string expression = null;
            try
            {
                expression = reader.ReadExpressionFromFile("test.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }
            Console.WriteLine(expression);
            var calculator = new ExpressionCalculator();
            Console.WriteLine(calculator.Calculate(expression));
            Console.ReadKey();
        }
    }
}
