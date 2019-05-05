using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionCalculation.Interfaces;
using ExpressionCalculation.Operands;

namespace ExpressionCalculation.Operations
{
    /// <summary>
    /// Класс, представляющий собой реализацию математической операции косинуса числа
    /// </summary>
    public class Cos : IOperation
    {
        /// <summary>
        /// Приоритет операции равнй двум
        /// </summary>
        public int Priority => 2;
        /// <summary>
        /// Количество операндов равное 1
        /// </summary>
        public int OperandsCount => 1;
        /// <summary>
        /// Коллекция операндов у операции, содержащего ровно 1 операнд
        /// </summary>
        public IList<IOperand> Operands { get; }
        /// <summary>
        /// Конструктор, инициализирующий объект операции косинуса и создающий коллекцию операндов
        /// </summary>
        public Cos()
        {
            Operands = new List<IOperand>();
        }
        /// <summary>
        /// Метод, выполняющий вычесление косинуса над 1 объектом, содержащимся в коллекции Operands
        /// </summary>
        /// <returns>Объект типа IOperand, в котором хранится результат вычисления косинуса числа</returns>
        /// <exception cref="InvalidOperationException">Происходит, если операция Operands не содержит операндов или является null или первый операнд является null</exception>
        public IOperand Execute()
        {
            if (Operands == null || !Operands.Any()) throw new InvalidOperationException("Нельзя выполнить операцию без операндов");
            if (Operands[0] == null) throw new InvalidOperationException("Один из операндов пуст или не существует");
            return new Operand((float)Math.Cos(Operands[0].Value));
        }
    }
}