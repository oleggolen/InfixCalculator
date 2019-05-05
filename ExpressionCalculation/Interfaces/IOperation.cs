using System.Collections.Generic;

namespace ExpressionCalculation.Interfaces
{
    /// <summary>
    /// Интерфейс, представляющий интерфейс объекта-операции, то есть объекта совершающего вычисление над операндами
    /// </summary>
    public interface IOperation : IExpressionElement 
    {
        /// <summary>
        /// Приоритет операции
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// Количество операндов у операции
        /// </summary>
        int OperandsCount { get; }
        /// <summary>
        /// Коллекция операндов у операции, содержащего ровно OperandCount операндов
        /// </summary>
        IList<IOperand> Operands { get; }
        /// <summary>
        /// Метод, выполняющий вычесление математической операции над OperandsCount объектов, содержащихся в коллекции Operands
        /// </summary>
        /// <returns>Объект типа IOperand, в котором хранится результат вычисления операции</returns>
        IOperand Execute();
    }
}
