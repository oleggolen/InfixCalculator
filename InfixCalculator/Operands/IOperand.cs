using InfixCalculator.Interfaces;

namespace InfixCalculator.Operands
{
    public interface IOperand : IExpressionElement
    {
        float Value { get; set; }
    }
}
