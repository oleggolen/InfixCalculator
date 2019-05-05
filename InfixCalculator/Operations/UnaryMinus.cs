using System;
using System.Collections.Generic;
using System.Linq;
using InfixCalculator.Operands;
namespace InfixCalculator.Operations
{
    /// <summary>
    /// Класс, представляющий собой реализацию математической операции унарного минуса числа, то есть взятия числа с противоположным знаком
    /// </summary>
    public class UnaryMinus : IOperation
    {
        /// <summary>
        /// Приоритет операции равнй четырём
        /// </summary>
        public int Priority => 4;
        /// <summary>
        /// Количество операндов равное 1
        /// </summary>
        public int OperandsCount => 1;
        /// <summary>
        /// Коллекция операндов у операции, содержащего ровно 1 операнд
        /// </summary>
        public IList<IOperand> Operands { get; }
        /// <summary>
        /// Конструктор, инициализирующий объект операции унарного минуса и создающий коллекцию операндов
        /// </summary>
        public UnaryMinus()
        {
            Operands = new List<IOperand>();
        }
        /// <summary>
        /// Метод, выполняющий вычесление унарного минуса над 1 объектом, содержащимся в коллекции Operands
        /// </summary>
        /// <returns>Объект типа IOperand, в котором хранится результат вычисления унарного минуса числа</returns>
        /// <exception cref="InvalidOperationException">Происходит, если операция Operands не содержит операндов или является null или первый операнд является null</exception>
        public IOperand Execute()
        {
            if (Operands == null || !Operands.Any()) throw new InvalidOperationException("Нельзя выполнить операцию без операндов");
            if (Operands[0] == null) throw new InvalidOperationException("Один из операндов пуст или не существует");
            return new Operand(-Operands[0].Value);
        }
    }
}