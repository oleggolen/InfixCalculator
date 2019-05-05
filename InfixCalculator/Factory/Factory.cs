using System;
using InfixCalculator.Brackets;
using InfixCalculator.Operands;
using InfixCalculator.Operations;
namespace InfixCalculator.Factory
{
    /// <summary>
    /// Класс, представляющий собой реализацию фабрики объектов
    /// </summary>
    public class Factory : IFactory
    {
        /// <summary>
        /// Метод, возвращающий объект-скобку
        /// </summary>
        /// <param name="type">Тип скобки '(" - открывающаяся скобка, ')' - закрывающаяся скобка</param>
        /// <returns>Объект, представляющий собой действие открывающийся скобки</returns>
        public IBracket CreateBracket(char type)
        {
            return type == '(' ? (IBracket)new OpenBracket() : new CloseBracket();
        }
        /// <summary>
        /// Метод создающий объект-операнд
        /// </summary>
        /// <param name="operand">строка представляющая собой операнд</param>
        /// <returns>Объект-операнд, представляющий собой объект хранящий целое число</returns>
        public IOperand CreateOperand(string operand)
        {
            float c = 0;
            if(!float.TryParse(operand,out c)) throw new ArgumentException("операнд не соответствует вещественному  числу");
            return new Operand(float.Parse(operand));

        }
        /// <summary>
        /// Метод, создающий объект-операцию
        /// </summary>
        /// <param name="operation">Выражение, представляющее собой операцию</param>
        /// <returns>Объект, представляющей собой реализацию математической операции</returns>
        public IOperation CreateOperation(string operation)
        {
            switch (operation)
            {
                case "+": return new Plus();
                case "-": return new Minus();
                case "*": return new Multiply();
                case "/": return new Divide();
                case "u-": return new UnaryMinus();
                case "sin": return new Sin();
                case "cos": return new Cos();
                default: throw  new InvalidOperationException("operation не является операцией");
            }
            
        }
    }
}