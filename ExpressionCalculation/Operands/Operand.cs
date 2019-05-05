using ExpressionCalculation.Interfaces;

namespace ExpressionCalculation.Operands
{
    /// <summary>
    /// Класс представляющий собой класс объектов-операндов, то есть объектов над которыми совершаются операции
    /// </summary>
    class Operand : IOperand
    {
        /// <summary>
        /// Конструктор, который инициализирует оббъекты-операнды
        /// </summary>
        /// <param name="value">Вещественное число, которое храниться в качестве значения операнда</param>
        public Operand(float value)
        {
            Value = value;
        }
        /// <summary>
        /// Значение операнда, то есть целое число, над которым можно содержать операции
        /// </summary>
        public float Value { get; set; }
    }
}
