using System;
using System.Collections.Generic;
using System.Linq;
using InfixCalculator.Operands;

namespace InfixCalculator.Operations
{
    /// <summary>
    /// Класс, представляющий собой реализацию математической операции вычисления разности двух чисел
    /// </summary>
    public class Minus : IOperation
    {
        /// <summary>
        /// Приоритет операции равнй двум
        /// </summary>
        public int Priority => 2;
        /// <summary>
        /// Коллекция операндов у операции, содержащего ровно 2 операнда
        /// </summary>
        public IList<IOperand> Operands { get; }
        /// <summary>
        /// Количество операндов равное 2
        /// </summary>
        public int OperandsCount => 2;
        /// <summary>
        /// Конструктор, инициализирующий объект операции взятия разности двух чисел и создающий коллекцию операндов
        /// </summary>
        public Minus()
        {
            Operands = new List<IOperand>();
        }
        /// <summary>
        /// Метод, выполняющий вычесление разности над 2 объектамм, содержащимся в коллекции Operands
        /// </summary>
        /// <returns>Объект типа IOperand, в котором хранится результат вычисления разности чисел</returns>
        /// <exception cref="InvalidOperationException">Происходит, если операция Operands не содержит операндов или является null или первый или второй операнд является null</exception>
        public IOperand Execute()
        {
            if (Operands == null || !Operands.Any()) throw new InvalidOperationException("Нельзя выполнить операцию без операндов");
            if (Operands[0] == null || Operands[1] == null) throw new InvalidOperationException("Один из операндов пуст или не существует");
            return new Operand(Operands[1].Value - Operands[0].Value);
        }
    }
}