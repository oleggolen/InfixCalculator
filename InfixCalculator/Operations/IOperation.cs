using System.Collections.Generic;
using InfixCalculator.Interfaces;
using InfixCalculator.Operands;

namespace InfixCalculator.Operations
{
    public interface IOperation : IExpressionElement
    {
        int Priority { get; }
        int OperandsCount { get; }
        IList<IOperand> Operands { get; }
        IOperand Execute();
    }
}
