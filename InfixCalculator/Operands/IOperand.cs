using InfixCalculator.Interfaces;

namespace InfixCalculator.Operands
{
    /// <summary>
    /// Интерфейс, представляющий собой интерфейс объекта-операнда, то есть числел, над которыми совершаются операции
    /// </summary>
    public interface IOperand : IExpressionElement
    {
        /// <summary>
        /// Значение операнда, то есть целое число, над которым можно содержать операции
        /// </summary>
        float Value { get; set; }
    }
}
